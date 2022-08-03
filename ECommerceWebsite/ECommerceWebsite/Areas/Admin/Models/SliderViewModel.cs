using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Areas.Admin.Models
{
    public class SliderViewModel
    {
        public IEnumerable<ECommerceWebsite.Models.Sliders> Sliders { get; set; }

        public ECommerceWebsite.Models.Sliders Slider { get; set; }
    }
}