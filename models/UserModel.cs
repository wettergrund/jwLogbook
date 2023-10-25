using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwLogbook.models 
{
    [Table("UserDetails")]
    public class UserModel : IdentityUser
    {

    }
}
