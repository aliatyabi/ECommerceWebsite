using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }
    }
}
