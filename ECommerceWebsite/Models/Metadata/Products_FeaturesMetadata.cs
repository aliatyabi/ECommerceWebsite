using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models.Metadata
{
    public class Products_FeaturesMetadata
    {
        [Key]
        public int ProductFeatureId { get; set; }
        public int ProductId { get; set; }
        public int FeatureId { get; set; }
        public int FeatureValueId { get; set; }

        public virtual Feature_Values Feature_Values { get; set; }
        public virtual Features Features { get; set; }
        public virtual Products Products { get; set; }
    }

    [MetadataType(typeof(Products_FeaturesMetadata))]
    public partial class Products_Features
    {

    }
}