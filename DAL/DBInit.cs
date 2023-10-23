namespace HouseRenting.DAL
{
    public class DBInit
    {

        public static void Seed(IApplicationBuilder app)
        {
            /*
          using var serviceScope = app.ApplicationServices.CreateScope();
            ItemDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItemDbContext>();
            //context.Database.EnsureDeleted();
            

            if (!context.Items.Any())
            {
                var items = new List<Item>
            {
                new Item
                {
            Category="House",
            Location ="Drammen",
            Rooms=6,
            Area = 150,
            Renting =19500,
            Description="A new build house",
            ImageUrl="/images/House1.jpg"
                },
                new Item
                {
                    Category="House",
            Location ="Drammen",
            Rooms=6,
            Area = 160,
            Renting =17500,
            Description="A new build house",
            ImageUrl="/images/House2.jpg"

                },
                new Item
                {
                    Category="House",
            Location ="Oslo",
            Rooms=6,
            Area = 170,
            Renting =18500,
            Description="A new build house",
            ImageUrl="/images/House3.jpg"

                }

            };
                context.AddRange(items);
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                var customers = new List<Customer>
            {
                new Customer { Name = "Jo Baiden", Email = "s111111@oslomet.no", Phone="99988777"},
                new Customer { Name = "Ben Salman", Email = "s222222@oslomet.no", Phone="55566333"},
            };
                context.AddRange(customers);
                context.SaveChanges();
            }

            if (!context.Bookings.Any())
            {
                var bookings = new List<Booking>
            {
                new Booking {BookingDate = DateTime.Today.ToString("yyyy-MM-dd"), ItemId = 1, CustomerId = 2},
                new Booking {BookingDate = DateTime.Today.ToString("yyyy-MM-dd"), ItemId = 2, CustomerId = 1}
                
            };
                context.AddRange(bookings);
                context.SaveChanges();
            }
          */

        }
    }
}
