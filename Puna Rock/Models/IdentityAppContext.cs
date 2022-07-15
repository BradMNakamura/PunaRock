using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Puna_Rock.Models
{
    public class IdentityAppContext : IdentityDbContext<AppUser, AppRoles, string>
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
        {

        }
    }
}
