using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public int ProductId { get; set; }
        public IEnumerable<int> SizeId { get; set; }
        public byte Quantity { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public int Fee { get; set; }
        public int DiscountPercent { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int FurtherSubCategoryId { get; set; }
        public int FurthestSubCategoryId { get; set; }
        public byte Status { get; set; }
        public string Comment { get; set; }
        public bool IsFavourite { get; set; }
    }
}
