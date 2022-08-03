using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models.Metadata
{
    public class Feature_ValuesViewModelMetadata
    {
        [Key]
        public int FeatureValueId { get; set; }

        public string FeatureValue { get; set; }

        public int FeatureId { get; set; }
    }

    [MetadataType(typeof(Feature_ValuesViewModelMetadata))]
    public partial class Feature_ValuesViewModel
    { }
}