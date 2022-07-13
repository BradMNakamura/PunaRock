using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Puna_Rock.Models
{
    public class AppUser : IdentityUser<int>
    {
        /*
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        */
    }
}
