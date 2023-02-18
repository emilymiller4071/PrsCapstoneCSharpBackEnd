using Microsoft.EntityFrameworkCore;

namespace PrsBackEndCSharp.Models
{
    public class PrsContext : DbContext   // not a POCO
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLine> RequestLines { get; set; }


        // constructor to support dependency injection (via a service)
        public PrsContext( DbContextOptions<PrsContext> options ) : base( options )
        {
        }


    }
}
