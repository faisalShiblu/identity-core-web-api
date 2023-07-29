using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Identity.API.Models.DbContext
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        public void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
           (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName="ADMIN" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName="USER" },
                new IdentityRole() { Name = "Manager", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName="MANAGER" }
            );
        }
    }
}
