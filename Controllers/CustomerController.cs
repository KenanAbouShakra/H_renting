using HouseRenting.DAL;
using HouseRenting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ItemDbContext _itemDbContext;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerController> _logger; 

        public CustomerController(ItemDbContext itemDbContext, ICustomerRepository customerRepository, ILogger<CustomerController> logger)
        {
            _itemDbContext = itemDbContext;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Table()
        {

            string loggedInUserEmail = User.Identity.Name;


            List<Customer> customers = await _itemDbContext.Customers
                .Where(c => c.Email == loggedInUserEmail)
                .ToListAsync();

            return View(customers);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                _logger.LogError("[CustomerController] Customer not found for the CustomerId {CustomerId:0000}", id);
                return BadRequest("Customer not found for the CustomerId");
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomerConfirmed(int id)
        {
            bool ok = await _customerRepository.DeleteCustomer(id);
            if (!ok)
            {
                _logger.LogError("[CustomerController] Customer deletion failed for the CustomerId { CustomerId:0000}", id);
                return BadRequest("Customer deletion failed");
            }
            return RedirectToAction(nameof(Table));
        }
    }

}
