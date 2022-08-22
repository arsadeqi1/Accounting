using Accounting.ViewModel.Customers;
using System.Collections.Generic;

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
        int GetCustomerIdByName(string name);
        string GetCustomerNameById(int customerId);
    }
}
