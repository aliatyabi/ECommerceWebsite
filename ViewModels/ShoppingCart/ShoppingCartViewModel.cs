using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public byte Quantity { get; set; }

        public int SizeId { get; set; }

        public string Size { get; set; }

        public int Fee { get; set; }

        public double DiscountPercent { get; set; }

        public int DiscountFee { get; set; }

        public int OldPrice { get; set; }

        public int Price { get; set; }

        public string ImageName { get; set; }
    }
}
