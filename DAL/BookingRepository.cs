using Microsoft.EntityFrameworkCore;
using HouseRenting.Models;
using HouseRenting.Migrations;

namespace HouseRenting.DAL
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ItemDbContext _db;
        private readonly ILogger<BookingRepository> _logger;
        public BookingRepository(ItemDbContext db, ILogger<BookingRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<bool> CreateBookingItem(Booking booking, int itemId)
        {
            try
            {
                _db.Bookings.Add(booking);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[BookingRepository] Booking creation failed, error message: {e}", booking, e.Message);
                return false;
            }

        }
        public async Task<Booking?> GetBookingById(int id)
        {
            try
            {
                return await _db.Bookings.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("[BookingRepository] Booking FindAsync(bookingId) failed when GetBookingById for BookingId {BookingId:0000}, error message: {e}", id, e.Message);
                return null;
            }
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                var booking = await _db.Bookings.FindAsync(id);
                if (booking == null)
                {
                    _logger.LogError("[BookingRepository] booking not found for the BookingId {BookingId:0000}", id);
                    return false;
                }
                var item = await _db.Items.FindAsync(booking.Items?.ItemId);
                item.IsBooked = false;
                await _db.SaveChangesAsync();
                _db.Bookings.Remove(booking);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[BookingRepository] booking deletion failed for the BookingId {BookingId:0000}, error message: {e}", id, e.Message);
                return false;
            }
        }



    }
}
