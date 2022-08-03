using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class Products_FeaturesMetadata
    {
        [Key]
        public int ProductFeatureId { get; set; }
        public int ProductId { get; set; }
        public int FeatureValueId { get; set; }
    }

    [MetadataType(typeof(Products_FeaturesMetadata))]
    public partial class Products_Features
    {

    }
}