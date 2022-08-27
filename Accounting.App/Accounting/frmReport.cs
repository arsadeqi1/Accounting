using Accounting.DataLayer.Context;
using Accounting.Utilties.Convertor;
using Accounting.ViewModel.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.App.Accounting
{
    public partial class frmReport : Form
    {
        public int TypeID = 0;
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            using(UnitOfwork db = new UnitOfwork())
            {
                List<ListCustomerViewModel> list = new List<ListCustomerViewModel>();
                list.Add(new ListCustomerViewModel
                {
                    CustomerId = 0,
                    FullName = "انتخاب کنید"
                });
                list.AddRange(db.CustomerRepository.GetNameCustomers());
                cbCustomers.DataSource = list;
                cbCustomers.DisplayMember = "FullName";
                cbCustomers.ValueMember = "CustomerId";
            }

            if (TypeID == 1)
            {
                this.Text = "گزارش دریافتی ها";
            }
            else
            {
                this.Text = "گزارش پرداختی ها";
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            using (UnitOfwork db = new UnitOfwork())
            {
                List<DataLayer.Accounting> result = new List<DataLayer.Accounting>();
                DateTime? startDate;
                DateTime? endDate;

                if((int) cbCustomers.SelectedValue != 0)
                {
                    int customerId = int.Parse(cbCustomers.SelectedValue.ToString());
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeID && a.CostomerID == customerId));
                }
                else
                {
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeID));
                }

                if(txtFromDate.Text != "  /  /")
                {
                    startDate = Convert.ToDateTime(txtFromDate.Text);
                    startDate = DateConvertor.ToMiladi(startDate.Value);
                    result = result.Where(r => r.DateTime >= startDate.Value).ToList();
                }
                if(txtToDate.Text != "  /  /")
                {
                    endDate = Convert.ToDateTime(txtToDate.Text);
                    endDate = DateConvertor.ToMiladi(endDate.Value);
                    result = result.Where(r => r.DateTime <= endDate.Value).ToList();
                }

                // dgReport.AutoGenerateColumns = false;
                // var result = db.AccountingRepository.Get(c => c.TypeID == TypeID);
                // dgReport.DataSource = result;

                dgReport.Rows.Clear();
                foreach (var accounting in result)
                {
                    string customerName = db.CustomerRepository.GetCustomerNameById(accounting.CostomerID);
                    dgReport.Rows.Add(accounting.ID, customerName, accounting.Amount, accounting.Description, accounting.DateTime.ToShamsi());
                }

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                if(RtlMessageBox.Show("آیا از حذف مطمءن هستید؟", "توجه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (UnitOfwork db = new UnitOfwork())
                    {
                        db.AccountingRepository.Delete(id);
                        db.Save();
                        Filter();
                    }
                }
            }
            else
            {
                RtlMessageBox.Show("Please select a row");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                frmNewAccounting frmNew = new frmNewAccounting();
                frmNew.AccountID = id;
                if(frmNew.ShowDialog() == DialogResult.OK)
                {
                    Filter();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row!");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint = new DataTable();
            dtPrint.Columns.Add("Customer");
            dtPrint.Columns.Add("Amount");
            dtPrint.Columns.Add("Date");
            dtPrint.Columns.Add("Description");

            foreach (DataGridViewRow item in dgReport.Rows)
            {
                dtPrint.Rows.Add(
                    item.Cells[0].Value.ToString(),
                    item.Cells[1].Value.ToString(),
                    item.Cells[2].Value.ToString(),
                    item.Cells[3].Value.ToString()
                    );
            }

            stiPrint.Load(Application.StartupPath + "/Report.mrt");
            stiPrint.RegData("DT", dtPrint);
            stiPrint.Show();
        }
    }
}
