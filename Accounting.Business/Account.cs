using Accounting.DataLayer.Context;
using Accounting.ViewModel.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Business
{
    public class Account
    {
        public static ReportViewModel ReportFormMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using(UnitOfwork db = new UnitOfwork())
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31);

                var receive = db.AccountingRepository.Get(a => a.TypeID == 1 && a.DateTime >= startDate && a.DateTime <= endDate).Select(a => a.Amount).ToList();
                var Pay = db.AccountingRepository.Get(a => a.TypeID == 2 && a.DateTime >= startDate && a.DateTime <= endDate).Select(a => a.Amount).ToList();

                rp.Receive = receive.Sum();
                rp.Pay = Pay.Sum();
                rp.AccountBalance = (receive.Sum() - Pay.Sum());
            }

            return rp;
        }
    }
}
