using Accounting.DataLayer.Repostories;
using Accounting.DataLayer.Services;
using System;

namespace Accounting.DataLayer.Context
{
    public class UnitOfwork : IDisposable
    {
        Accounting_DBEntities db = new Accounting_DBEntities();

        private ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(db);
                }
                return _customerRepository;
            }
        }

        private GenericRepository<Accounting> _accountingRepository;
        public GenericRepository<Accounting> AccountingRepository
        {
            get
            {
                if (_accountingRepository == null)
                {
                    _accountingRepository = new GenericRepository<Accounting>(db);
                }
                return _accountingRepository;
            }
        }

        private GenericRepository<Login> _loginRepository;
        public GenericRepository<Login> LoginRepository
        {
            get
            {
                if(_loginRepository == null)
                {
                    _loginRepository = new GenericRepository<Login>(db);
                }
                return _loginRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
