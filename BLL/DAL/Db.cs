using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<BlogTag> BlogTags { get; set; }

        public Db(DbContextOptions options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set precision for the Rating property in Blog model
            modelBuilder.Entity<Blog>()
                .Property(b => b.Rating)
                .HasPrecision(18, 2);  // Precision: 18 digits in total, 2 digits after the decimal point
        }
    }
}
