using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class Product_GalleryMetadata
    {
        [Key]
        public int GalleryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProductRequired")]
        [Display(Name = "Product", ResourceType = typeof(Resources.Resource))]
        public int ProductId { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Resources.Resource))]
        public string ImageName { get; set; }
    }

    [MetadataType(typeof(Product_GalleryMetadata))]
    public partial class Product_Gallery
    { }
}