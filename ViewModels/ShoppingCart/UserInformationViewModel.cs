using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ShoppingCart
{
    public class UserInformationViewModel
    {
        [Key]
        public int InfoId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameRequired")]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameLength")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LastNameRequired")]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LastNameLength")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AddressRequired")]
        [Display(Name = "Address", ResourceType = typeof(Resources.Resource))]
        [MaxLength(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AddressLength")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TelephoneRequired")]
        [Display(Name = "Telephone", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TelephoneLength")]
        public string Telephone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileRequired")]
        [Display(Name = "Mobile", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileLength")]
        public string Mobile { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CityRequired")]
        [Display(Name = "City", ResourceType = typeof(Resources.Resource))]
        public string City { get; set; }
    }
}
