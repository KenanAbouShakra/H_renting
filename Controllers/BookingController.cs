using HouseRenting.DAL;
using HouseRenting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseRenting.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HouseRenting.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<BookingController> _logger;
        private readonly ItemDbContext _itemDbContext;

        public BookingController(ItemDbContext itemDbContext, IBookingRepository bookingRepository, ILogger<BookingController> logger)
        {
            _itemDbContext = itemDbContext;
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Table()
        {
            string loggedInUserEmail = User.Identity.Name;
            List<Booking> bookings = await _itemDbContext.Bookings
                .Where(b => b.Customer.Email == loggedInUserEmail)
                .ToListAsync();
            return View(bookings);
        }


        [HttpGet]
        [Authorize]
        public IActionResult CreateBookingItem(int itemId)
        {
            // Fetch the Item using the itemId
            var item = _itemDbContext.Items.FirstOrDefault(i => i.ItemId == itemId);

            if (item == null)
            {
                // Handle the case where the item is not found, maybe return a 404 or redirect
                return NotFound();
            }

            // Create a new Booking object and set its ItemId
            Booking booking = new Booking { ItemId = itemId };

            // Create the view model
            var viewModel = new CreateBookingViewModel
            {
                Item = item,
                Booking = booking
            };

            
            return View(viewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBookingItem(Booking booking, int itemId)
        {
            // Fetch the Item using the itemId
            var item = _itemDbContext.Items.FirstOrDefault(i => i.ItemId == itemId);

            // Create the view model
            var viewModel = new CreateBookingViewModel
            {
                Item = item,
                Booking = booking
            };

            if (ModelState.IsValid)
            {
                // Check if booking was created successfully
                bool isBookingCreated = await _bookingRepository.CreateBookingItem(booking, itemId);

                if (isBookingCreated)
                {
                    booking.Items.IsBooked = true;

                    await _itemDbContext.SaveChangesAsync();
                    // Table message
                    string tableHtml = $"<table class=\"table table-striped\">" +
                        $"<tr><th>House number</th><th>Customer Name</th><th>Booking Date</th></tr>" +
                        $"<tr><td>{booking.ItemId}</td><td>{booking.Customer.Name}</td><td>{booking.BookingDate}</td></tr>" +
                        $"</table>";

                    TempData["BookingTable"] = tableHtml;

                    // Confirmation message without the table
                    string confirmationMessage = $"We confirm that Huset nummer {booking.ItemId} er booket av {booking.Customer.Name} in the date:{booking.BookingDate}!";

                    TempData["BookingConfirmation"] = confirmationMessage;

                    // Redirect to Receipt view
                    return View("Receipt", viewModel);
                }
            }

            // If there are validation errors or booking creation failed, set ModelState for ItemId and return to the same view with the validation errors
            _logger.LogWarning("[BookingController] Booking creation failed", booking);

          

            return View("CreateBookingItem", viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                _logger.LogError("[BookingController] Booking not found for the BookingId {BookingId:0000}", id);
                return BadRequest("Booking not found for the BookingId");
            }
            return View(booking);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool Ok = await _bookingRepository.Delete(id);
            if (!Ok)
            {
                _logger.LogError("[BookingController] Booking deletion failed for the  BookingId { BookingId:0000}", id);
                return BadRequest(" Booking deletion failed");
            }
            return RedirectToAction(nameof(Table));
        }

       
    }
}
