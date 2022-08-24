using Accounting.DataLayer.Context;
using System;
using System.Linq;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmLogin : Form
    {
        public bool isEdit = false;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOfwork db = new UnitOfwork())
                {
                    if (isEdit)
                    {
                        var login = db.LoginRepository.Get().First();
                        login.UserName = txtUserName.Text;
                        login.Password = txtPassword.Text;
                        db.LoginRepository.Update(login);
                        db.Save();
                        Application.Restart();
                    }
                    else
                    {
                        if (db.LoginRepository.Get(l => l.UserName == txtUserName.Text && l.Password == txtPassword.Text).Any())
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("User Not found!!!");
                        }
                    }

                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (isEdit)
            {
                this.Text = "تنظیمات ورود به برنامه";
                btnLogin.Text = "ذخیره";
                using (UnitOfwork db = new UnitOfwork())
                {
                    var login = db.LoginRepository.Get().First();
                    txtUserName.Text = login.UserName;
                    txtPassword.Text = login.Password;
                }
            }
        }
    }
}
