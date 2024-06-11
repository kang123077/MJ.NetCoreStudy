using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AspnetNote.MVC.Models
{
    public class IdenRole : IdentityRole<long>
    {
        public IdenRole()
        {

        }
        public IdenRole(string roleName)
        : base(roleName)
        {

        }
    }
}
