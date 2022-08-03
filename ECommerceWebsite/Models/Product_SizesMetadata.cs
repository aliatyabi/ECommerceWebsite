using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class Product_SizesMetadata
    {
        [Key]
        public int SizeId { get; set; }
        public string SizeUK { get; set; }
        public string SizeIR { get; set; }
        public int SizeGroup { get; set; }
    }

    [MetadataType(typeof(Product_SizesMetadata))]
    public partial class Product_Sizes
    { }
}