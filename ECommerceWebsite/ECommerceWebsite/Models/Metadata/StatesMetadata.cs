using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models.Metadata
{
    public class StatesMetadata
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "StateRequired")]
        public string StateName { get; set; }
    }

    [MetadataType(typeof(StatesMetadata))]
    public partial class States
    { }
}