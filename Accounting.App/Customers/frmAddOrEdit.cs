using Accounting.DataLayer.Context;
using Accounting.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using System.IO;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Accounting.App.Customers
{
    public partial class frmAddOrEdit : Form
    {
        public int customerID = 0;
        UnitOfwork db = new UnitOfwork();

        public frmAddOrEdit()
        {
            InitializeComponent();
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            if(OpenFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation = OpenFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string Pathh = Application.StartupPath + "/Images/";

                if (!Directory.Exists(Pathh))
                {
                    Directory.CreateDirectory(Pathh);
                }
                pcCustomer.Image.Save(Pathh + imageName);

                DataLayer.Customers customer = new DataLayer.Customers()
                {
                    FullName = txtName.Text,
                    Email = txtEmail.Text,
                    Mobile = txtMobile.Text,
                    Address = txtAddress.Text,
                    Image = imageName
                };

                if(customerID == 0)
                {
                    db.CustomerRepository.InsertCustomer(customer);
                }
                else
                {
                    customer.CustomerID = customerID;
                    db.CustomerRepository.UpdateCustomer(customer);
                }

                db.Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void frmAddOrEdit_Load(object sender, EventArgs e)
        {
            if(customerID != 0)
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";

                var customer = db.CustomerRepository.GetCustomerById(customerID);
                txtName.Text = customer.FullName;
                txtMobile.Text = customer.Mobile;
                txtEmail.Text = customer.Email;
                txtAddress.Text = customer.Address;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.Image;
            }
        }
    }
}
