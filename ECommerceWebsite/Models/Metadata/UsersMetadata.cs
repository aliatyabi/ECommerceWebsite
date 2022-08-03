using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models.Metadata
{
    public class UsersMetadata
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RoleIdRequired")]
        [Display(Name = "RoleId", ResourceType = typeof(Resources.Resource))]
        public int RoleId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.EmailAddress,ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [MinLength(length:8,ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "PasswordMinLength")]
        public string Password { get; set; }

        [Display(Name="ActivationCode",ResourceType = typeof(Resources.Resource))]
        public string ActivationCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "IsActiveRequired")]
        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resource))]
        public bool IsActive { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RegisterationDateRequired")]
        [Display(Name = "RegisterationDate", ResourceType = typeof(Resources.Resource))]
        public System.DateTime RegisterationDate { get; set; }
    }

    [MetadataType(typeof(UsersMetadata))]
    public partial class Users
    { }
}