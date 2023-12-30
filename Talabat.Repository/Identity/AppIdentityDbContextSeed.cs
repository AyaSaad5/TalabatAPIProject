using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites.Identitiy;

namespace Talabat.Repository.Identity
{
    public  class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManger)
        {
            if(!userManger.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName="Aya Saad",
                    Email="ayasa3d5@gmail.com",
                    UserName="ayasa3d5",
                    PhoneNumber="01025190810"
                };
                await userManger.CreateAsync(user,"ayaSa3d5@");

            }
        }
    }
}
