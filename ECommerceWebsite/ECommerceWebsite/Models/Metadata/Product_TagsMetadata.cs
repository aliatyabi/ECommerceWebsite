using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models.Metadata
{
    public class Product_TagsMetadata
    {
        [Key]
        public int TagId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProductRequired")]
        [Display(Name = "Product", ResourceType = typeof(Resources.Resource))]
        public int ProductId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "MetaTitle", ResourceType = typeof(Resources.Resource))]
        public string MetaTitle { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MetaDescriptionRequired")]
        [Display(Name = "MetaDescription", ResourceType = typeof(Resources.Resource))]
        [MaxLength(160, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MetaDescriptionLength")]
        public string MetaDescription { get; set; }

        [Display(Name = "MetaKeywords", ResourceType = typeof(Resources.Resource))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MetaKeywordsLength")]
        public string MetaKeyword { get; set; }
    }

    [MetadataType(typeof(Product_TagsMetadata))]
    public partial class Product_Tags
    { }
}