using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class Feature_ValuesMetadata
    {
        [Key]
        public int FeatureValueId { get; set; }
        public int FeatureId { get; set; }
        public string FeatureValue { get; set; }
        public string FeatureSimpleValue { get; set; }
    }

    [MetadataType(typeof(Feature_ValuesMetadata))]
    public partial class Feature_Values
    { }
}