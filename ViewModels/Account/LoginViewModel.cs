using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "UsernameRequired")]
        [Display(Name = "Username", ResourceType = typeof(Resources.Resource))]
        [MinLength(length: 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "UsernameMinLength")]
        [MaxLength(length: 20, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "UsernameMaxLength")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RememberMeLabel")]
        [Display(Name = "RememberMeLabel", ResourceType = typeof(Resources.Resource))]
        public bool RememberMe { get; set; }
    }
}
