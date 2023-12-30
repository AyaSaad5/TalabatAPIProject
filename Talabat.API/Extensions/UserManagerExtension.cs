using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Enitites.Identitiy;

namespace Talabat.API.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task <AppUser> FindUserWithaddressByEmailasync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U => U.Email == email);
            return user;
        }
    }
}
