using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class CitiesMetadata
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CityRequired")]
        public string CityName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "StateRequired")]
        public int StateId { get; set; }
    }

    [MetadataType(typeof(CitiesMetadata))]
    public partial class Cities
    { }
}