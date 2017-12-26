using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InventoryManagement.Library.Resources;
using System.Linq;
using System.Web;
using InventoryManagementLibrary.Helpers;

namespace InventoryManagement.Web.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Label))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Label))]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Label))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "MobileNo", ResourceType = typeof(Label))]
        public string MobileNo { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(Label))]
        public string Password { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Label))]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Role", ResourceType = typeof(Label))]
        public short RoleId { get; set; }


        public IEnumerable<DropdownHelper> Roles { get; set; }
    }

    public class LoginModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Label))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(Label))]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }

}