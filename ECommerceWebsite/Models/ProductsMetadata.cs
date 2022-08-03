using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class ProductsMetadata
    {
        [Key]
        [Display(Name = Resources.Strings.ResourceKeys.ProductId, ResourceType = typeof(Resources.Resource))]
        public int ProductId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = Resources.Strings.ResourceKeys.CategoryRequired)]
        [Display(Name = Resources.Strings.ResourceKeys.CategoryTitle, ResourceType = typeof(Resources.Resource))]
        public int CategoryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SubCategoryRequired")]
        [Display(Name = "SubCategory", ResourceType = typeof(Resources.Resource))]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FurtherSubCategoryRequired")]
        [Display(Name = "FurtherSubCategory", ResourceType = typeof(Resources.Resource))]
        public int FurtherSubCategoryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FurthestSubCategoryRequired")]
        [Display(Name = "FurthestSubCategory", ResourceType = typeof(Resources.Resource))]
        public int FurthestSubCategoryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "Title", ResourceType = typeof(Resources.Resource))]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ArticleRequired")]
        [Display(Name = "Article", ResourceType = typeof(Resources.Resource))]
        public string Article { get; set; }

        [Display(Name = "ShortDescription", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ShortDescriptionRequired")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ShortDescriptionLength")]
        public string ShortDescription { get; set; }

        [Display(Name = "Text", ResourceType = typeof(Resources.Resource))]
        public string Text { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Resources.Resource))]
        public string ImageName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FeeRequired")]
        [Display(Name = "Fee", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:#,0}")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FeeMustGreater")]
        public int Fee { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DiscountPercentRequired")]
        [Display(Name = "DiscountPercent", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:p}")]
        [Range(0, 100, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DiscountPercentMustBetween")]
        public int DiscountPercent { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Resource))]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public byte Status { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(Resources.Resource))]
        public string Comment { get; set; }
    }

    [MetadataType(typeof(ProductsMetadata))]
    public partial class Products
    { }
}