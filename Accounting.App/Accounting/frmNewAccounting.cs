using Accounting.DataLayer.Context;
using System;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewAccounting : Form
    {
        UnitOfwork db = new UnitOfwork();
        public int AccountID = 0;

        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNameCustomers();

            if (AccountID != 0)
            {
                var account = db.AccountingRepository.GetById(AccountID);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Description.ToString();
                txtName.Text = db.CustomerRepository.GetCustomerNameById(account.CostomerID);
                if (account.TypeID == 1)
                {
                    rbReceive.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }

                this.Text = "ویرایش تراکنش";
                btnSave.Text = "ویرایش";
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNameCustomers(txtFilter.Text);
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbReceive.Checked || rbPay.Checked)
                {
                    DataLayer.Accounting accounting = new DataLayer.Accounting()
                    {
                        Amount = int.Parse(txtAmount.Value.ToString()),
                        DateTime = DateTime.Now,
                        Description = txtDescription.Text,
                        CostomerID = db.CustomerRepository.GetCustomerIdByName(txtName.Text),
                        TypeID = (rbReceive.Checked) ? 1 : 2
                    };

                    if (AccountID == 0)
                    {
                        db.AccountingRepository.Insert(accounting);
                        db.Save();
                    }
                    else
                    {
                        using(UnitOfwork db2 = new UnitOfwork())
                        {
                            accounting.ID = AccountID;
                            db2.AccountingRepository.Update(accounting);
                            db2.Save();
                        }
                    }

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    RtlMessageBox.Show("لطفا نوع تراکنش را مشخص کنید");
                }
            }
        }
    }
}
