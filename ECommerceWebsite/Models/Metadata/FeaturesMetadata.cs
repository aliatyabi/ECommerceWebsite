using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models.Metadata
{
    public class FeaturesMetadata
    {
        [Key]
        public int FeatureId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FeatureTitleRequired")]
        [Display(Name = "FeatureTitle", ResourceType = typeof(Resources.Resource))]
        public string FeatureTitle { get; set; }
    }

    [MetadataType(typeof(FeaturesMetadata))]
    public partial class Features
    {

    }
}