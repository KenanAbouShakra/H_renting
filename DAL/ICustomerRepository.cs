using HouseRenting.Models;

namespace HouseRenting.DAL
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerById(int id);
        Task<bool> DeleteCustomer(int id);
    }
}
