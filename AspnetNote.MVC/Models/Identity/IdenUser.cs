using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AspnetNote.MVC.Models
{
    public class IdenUser : IdentityUser<long>
    {
    }
}
