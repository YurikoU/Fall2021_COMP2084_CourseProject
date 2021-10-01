using Fall2021_COMP2084_CourseProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fall2021_COMP2084_CourseProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        //Declare global references for all models to interact with the database
        public DbSet<City> Cities { get; set; }
        public DbSet<Post> Posts { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
