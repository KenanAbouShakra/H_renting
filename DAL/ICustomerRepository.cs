using HouseRenting.Models;

namespace HouseRenting.DAL
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer?> GetCustomerById(int id);
        Task<bool> DeleteCustomer(int id);
        Task<bool> DeleteAllCustomers();
    }
}
