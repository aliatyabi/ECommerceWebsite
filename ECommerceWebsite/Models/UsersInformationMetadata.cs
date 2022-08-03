using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class UsersInformationMetadata
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
        [MaxLength(500, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "AddressLength")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileRequired")]
        [Display(Name = "Mobile", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MobileLength")]
        public string Mobile { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CityRequired")]
        [Display(Name = "City", ResourceType = typeof(Resources.Resource))]
        public int CityId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NationalCodeRequired")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NationalCodeLength")]
        [Display(Name = "NationalCode", ResourceType = typeof(Resources.Resource))]
        public string NationalCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TelephoneRequired")]
        [Display(Name = "Telephone", ResourceType = typeof(Resources.Resource))]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TelephoneLength")]
        public string Tel { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "BirthdayNotValid")]
        public Nullable<System.DateTime> Birthday { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "GenderRequired")]
        [Display(Name = "Gender", ResourceType = typeof(Resources.Resource))]
        public Gender Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "StateRequired")]
        [Display(Name = "State", ResourceType = typeof(Resources.Resource))]
        public int StateId { get; set; }

        [Display(Name = "NewsletterMembership", ResourceType = typeof(Resources.Resource))]
        public bool NewsletterMembership { get; set; }

    }

    [MetadataType(typeof(UsersInformationMetadata))]
    public partial class UsersInformation
    {
    }
}