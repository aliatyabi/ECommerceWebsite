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
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "UsernameRequired")]
        [Display(Name = "Username", ResourceType = typeof(Resources.Resource))]
        [MinLength(length: 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "UsernameMinLength")]
        [MaxLength(length: 20, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "UsernameMaxLength")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [MinLength(length: 8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordMinLength")]
        [MaxLength(length: 150, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordMaxLength")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ConfirmPasswordRequired")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "ComparePassword")]
        public string ComparePassword { get; set; }
    }
}
