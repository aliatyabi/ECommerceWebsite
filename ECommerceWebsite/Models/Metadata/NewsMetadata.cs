using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWebsite.Models.Metadata
{
    public class NewsMetadata
    {
        [Key]
        public int NewsId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NewsTitleRequired")]
        [Display(Name = "NewsTitle", ResourceType = typeof(Resources.Resource))]
        [MaxLength(250,ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "NewsTitleLength")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ShortDescriptionRequired")]
        [Display(Name = "NewsShortDescription", ResourceType = typeof(Resources.Resource))]
        [MaxLength(300, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ShortDescriptionLength")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Display(Name = "NewsText", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        [Display(Name = "NumberOfVisits", ResourceType = typeof(Resources.Resource))]
        public int NumberOfVisits { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Resource))]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Resources.Resource))]
        public string ImageName { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resource))]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(NewsMetadata))]
    public partial class News
    {}
}