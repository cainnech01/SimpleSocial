using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FTsR.Models
{
    public class UserModel : IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }
        public int Year { get; set; }
    }
}
