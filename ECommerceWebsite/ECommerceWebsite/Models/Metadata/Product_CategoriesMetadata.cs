using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models.Metadata
{
    public class Product_CategoriesMetadata
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CategoryTitleRequired")]
        [Display(Name = "CategoryTitle", ResourceType = typeof(Resources.Resource))]
        public string CategoryTitle { get; set; }

        [Display(Name = "Parent", ResourceType = typeof(Resources.Resource))]
        public Nullable<int> ParentId { get; set; }
    }

    [MetadataType(typeof(Product_CategoriesMetadata))]
    public partial class Product_Categories
    { }
}