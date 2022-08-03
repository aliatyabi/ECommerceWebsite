using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [MinLength(length: 8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordMinLength")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ConfirmPasswordRequired")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "ComparePassword")]
        public string ComparePassword { get; set; }
    }
}
