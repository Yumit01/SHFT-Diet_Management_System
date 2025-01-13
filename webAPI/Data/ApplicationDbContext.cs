using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webAPI.Models;

namespace webAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DietPlan>? DietPlans { get; set; }
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Dietitian>? Dietitians { get; set; }
        public DbSet<Meal>? Meals { get; set; }
    }
}