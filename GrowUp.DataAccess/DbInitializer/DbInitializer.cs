using GrowUp.DataAccess.Data;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;


        public DbInitializer(

            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext db)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            // migrations if they are not appliyed 

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            // creat rools if they are not created 


            if (!_roleManager.RoleExistsAsync(StaticDetail.Role_Admin).GetAwaiter().GetResult())
            {

                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_User_Indi)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_Employee)).GetAwaiter().GetResult();

                //if roles are not created then , we will create admin user as well 

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "yassiradmin@gmail.com",
                    Email = "yassiradmin@gmail.com",
                    Name = "Yassir",
                    PhoneNumber = "0656893147",
                    PostalCode = "40000",
       

                }, "Admin@123456").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "yassiradmin@gmail.com");

                _userManager.AddToRoleAsync(user, StaticDetail.Role_Admin).GetAwaiter().GetResult();

            }
            return;


        }
    }
}
