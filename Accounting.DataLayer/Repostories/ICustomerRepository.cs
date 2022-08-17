using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Repostories
{
    public interface ICustomerRepository
    {
        List<Customers> GetAllCustomers();
        List<Customers> GetCustomerById(int customer);
        bool InsertCustomer(Customers customer);
        bool UpdateCustomer(Customers customer);
        bool DeleteCustomer(Customers customer);
        bool DeleteCustomer(int customerId);
        void Save();
    }
}
