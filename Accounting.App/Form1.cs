using Accounting.App.Accounting;
using Accounting.App.Customers;
using Accounting.Business;
using Accounting.Utilties.Convertor;
using Accounting.ViewModel.Accounting;
using System;
using System.Windows.Forms;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frmCustomers = new frmCustomers();
            frmCustomers.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmNewAccounting frmNewAccounting = new frmNewAccounting();
            frmNewAccounting.ShowDialog();
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 2;
            frmReport.ShowDialog();
        }

        private void btnReportReceive_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 1;
            frmReport.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frmlogin = new frmLogin();
            if (frmlogin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                lblDate.Text = DateConvertor.ToShamsi(DateTime.Now);
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss");

                report();
            }
            else
            {
                Application.Exit();
            }
        }

        void report()
        {
            ReportViewModel report = Account.ReportFormMain();
            lblReceive.Text = report.Receive.ToString("#,0");
            lblPay.Text = report.Pay.ToString("#,0");
            lblBalance.Text = report.AccountBalance.ToString("#,0");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnEditLogin_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.isEdit = true;
            frmLogin.ShowDialog();
        }
    }
}
