using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "OldPassword", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [MinLength(length: 8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordMinLength")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ConfirmPasswordRequired")]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ComparePassword")]
        public string ComparePassword { get; set; }
    }
}
