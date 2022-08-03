using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Product
{
    public class ProductFilterViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Text { get; set; }

        public int? FeeFrom { get; set; }

        public int? FeeTo { get; set; }

        public int? DiscountPercent { get; set; }
    }
}
