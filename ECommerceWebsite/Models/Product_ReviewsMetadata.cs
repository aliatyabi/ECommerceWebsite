using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class Product_ReviewsMetadata
    {
        [Key]
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public Nullable<short> Rating { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = Resources.Strings.ResourceKeys.ReviewRequired)]
        [StringLength(maximumLength: 500, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = Resources.Strings.ResourceKeys.ReviewMax)]
        public string ReviewText { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ParentId { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }

    [MetadataType(typeof(Product_ReviewsMetadata))]
    public partial class Product_Reviews
    {

    }
}