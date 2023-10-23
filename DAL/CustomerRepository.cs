using HouseRenting.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ItemDbContext _db;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(ItemDbContext db, ILogger<CustomerRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogError("[CustomerRepository] Invalid CustomerId provided: {CustomerId}", id);
                    return null;
                }

                return await _db.Customers.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("[CustomerRepository] Customer FindAsync(customerId) failed when GetCustomerById for CustomerId {CustomerId:0000}, error message: {e}, stack trace: {stackTrace}", id, e.Message, e.StackTrace);
                return null;
            }
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogError("[CustomerRepository] Invalid CustomerId provided for deletion: {CustomerId}", id);
                    return false;
                }

                var customer = await _db.Customers.FindAsync(id);
                if (customer == null)
                {
                    _logger.LogError("[CustomerRepository] Customer not found for the CustomerId {CustomerId:0000}", id);
                    return false;
                }

                var bookings = await _db.Bookings.Where(b => b.CustomerId == customer.CustomerId).ToListAsync();

                foreach (var booking in bookings)
                {
                    var item = await _db.Items.FindAsync(booking.ItemId);

                    if (item != null)
                    {
                        item.IsBooked = false;
                    }
                    _db.Customers.RemoveRange(bookings.Select(b => b.Customer));
                }

                _db.Customers.RemoveRange(customer);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[CustomerRepository] Customer deletion failed for the CustomerId {CustomerId:0000}, error message: {e}, stack trace: {stackTrace}", id, e.Message, e.StackTrace);
                return false;
            }
        }

    }
}
