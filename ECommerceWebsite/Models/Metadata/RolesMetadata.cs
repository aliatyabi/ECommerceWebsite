using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class RolesMetadata
    {
        [Key]
        public int RoleId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "RoleTitleRequired")]
        [Display(Name = "RoleTitle", ResourceType = typeof(Resources.Resource))]
        public string RoleTitle { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RoleNameRequired")]
        [Display(Name = "RoleName", ResourceType = typeof(Resources.Resource))]
        public string RoleName { get; set; }
    }

    [MetadataType(typeof(RolesMetadata))]
    public partial class Roles
    {
    }
}