using Accounting.DataLayer.Context;
using Accounting.Utilties.Convertor;
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
                // dgReport.AutoGenerateColumns = false;
                var result = db.AccountingRepository.Get(c => c.TypeID == TypeID);
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
    }
}
