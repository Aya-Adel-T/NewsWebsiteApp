using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using NewsWebsiteAPI.Models.Authentication;
using System.Collections.Generic;

namespace NewsWebsiteAPI.Data
{
    public class ElDbContext :IdentityDbContext<IdentityUser>
    {
        public ElDbContext(DbContextOptions<ElDbContext> options) : base(options) { }
        public DbSet<News> News { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            seedRoles(builder);
        }
        private void seedRoles(ModelBuilder builder) {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" });
        }
        //private void seedAdmins(ModelBuilder builder)
        //{
        //    PasswordHandler passwordHandler = new PasswordHandler();
        //    builder.Entity<IdentityUser>().HasData(
        //        new IdentityUser() { 
        //      UserName = "Aya",
        //      Email = "ayaadelt3@gmail.com", PasswordHash = passwordHandler.Hash("sdfsdf")
        //        });

        //}

    }
}
