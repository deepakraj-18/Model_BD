using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.Models
{
    public class UserDetailModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public long? RoleId { get; set; }
    }
    public class AddUserDetailModel
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public long? RoleId { get; set; }
    }
    public class EditUserDetailModel
    {
        [Required]
        public long Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }

    }

    public class LoginAdminModel
    {
        [Required]
        public string EmailAndMobile { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginAgentModel
    {
        [Required]
        public string EmailAndMobile { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginModelModel
    {
        [Required]
        public string EmailAndMobile { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
