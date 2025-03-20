using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Domain.Entities;

namespace MyWebApplication.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Cases> Cases { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            string adminName = "admin";
            string roleAdminId = "65fac756-7cc1-45a9-a558-1ec1380d9fd3";
            string userAdminId = "f464bd3f-b68f-46cb-a06f-445e4c2a4d89";
            string Email = "olegoviz.2006@gmail.com";


            //добавляем роль администратора сайта
            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = roleAdminId,
                Name = adminName,
                NormalizedName = adminName.ToUpper()
            });
            //добавляем нового IdentityUser в качестве администратора сайта
            builder.Entity<IdentityUser>().HasData(new IdentityUser()
            {
                Id = userAdminId,
                UserName = adminName,
                NormalizedUserName = adminName.ToUpper(),
                Email = Email,
                NormalizedEmail = Email.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser(),adminName),
                SecurityStamp = string.Empty,
                PhoneNumberConfirmed = true
            });
            //
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                RoleId = roleAdminId,
                UserId = userAdminId,
            });
        }
    }
}
