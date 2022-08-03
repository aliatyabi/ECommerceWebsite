using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Account
{
    public class ChangeProfileViewModel
    {
        [Key]
        public int UserId { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameRequired")]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FirstNameLength")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LastNameRequired")]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LastNameLength")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NationalCodeRequired")]
        [Display(Name = "NationalCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NationalCodeLength")]
        public string NationalCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TelephoneRequired")]
        [Display(Name = "Telephone", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TelephoneLength")]
        public string Tel { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileRequired")]
        [Display(Name = "Mobile", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileLength")]
        public string Mobile { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> Birthday { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "GenderRequired")]
        [Display(Name = "Gender", ResourceType = typeof(Resources.Resource))]
        public bool Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "StateRequired")]
        [Display(Name = "State", ResourceType = typeof(Resources.Resource))]
        public StatesViewModel State { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CityRequired")]
        [Display(Name = "City", ResourceType = typeof(Resources.Resource))]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AddressRequired")]
        [Display(Name = "Address", ResourceType = typeof(Resources.Resource))]
        [MaxLength(500,ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AddressLength")]
        public string Address { get; set; }
        public string Avatar { get; set; }
        public bool NewsletterMembership { get; set; }
    }
}
