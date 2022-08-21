using Accounting.ViewModel.Customers;
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
        Customers GetCustomerById(int customerId);
        IEnumerable<Customers> GetCustomerByFilter(string parameter);
        List<ListCustomerViewModel> GetNameCustomers(string filter = "");

        bool InsertCustomer(Customers customer);
        bool UpdateCustomer(Customers customer);
        bool DeleteCustomer(Customers customer);
        bool DeleteCustomer(int customerId);
    }
}
