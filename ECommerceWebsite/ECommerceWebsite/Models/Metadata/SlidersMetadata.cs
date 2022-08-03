using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models.Metadata
{
    public class SlidersMetadata
    {
        [Key]
        public int SlideId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SlideTitleRequired")]
        [Display(Name = "SlideTitle", ResourceType = typeof(Resources.Resource))]
        public string SlideTitle { get; set; }

        [Display(Name = "SlideUrl", ResourceType = typeof(Resources.Resource))]
        [Url(ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "SlideLinkMustBeUrl")]
        public string Link { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SlideImageRequired")]
        [Display(Name = "SlideImage", ResourceType = typeof(Resources.Resource))]
        public string Image { get; set; }

        [Display(Name = "SlideImage", ResourceType = typeof(Resources.Resource))]
        public int ClickCount { get; set; }

        [Display(Name = "SlideNewTab", ResourceType = typeof(Resources.Resource))]
        public bool NewTab { get; set; }
    }

    [MetadataType(typeof(SlidersMetadata))]
    public partial class Sliders
    { }
}