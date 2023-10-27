using HouseRenting.DAL;
using HouseRenting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

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

            string loggedInUserEmail = User.FindFirstValue(ClaimTypes.Email);


            List<Customer> customers = await _itemDbContext.Customers
                .Where(c => c.Email == loggedInUserEmail)
                .ToListAsync();

            return View(customers);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllCustomersTable()
        {
            //string loggedInUserEmail = User.Identity.Name;
            List<Customer> allCustomers = await _itemDbContext.Customers
                 .ToListAsync();
            return View(allCustomers);
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            var customers = await _customerRepository.GetAll();
            if (customers == null)
            {
                _logger.LogError("[CustomerController] Customers not found");
                return BadRequest("Customers not found");
            }

            return View("DeleteAllCustomersConfirmation");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllCustomersConfirmed()
        {
            bool ok = await _customerRepository.DeleteAllCustomers();

            if (!ok)
            {
                _logger.LogError("[CustomerController] Customers deletion failed ");
                return BadRequest("Customer deletion failed");
            }

            return RedirectToAction(nameof(AllCustomersTable));
        }

    }
}

