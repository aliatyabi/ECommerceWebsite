using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class Chain_Stock
    {
        public int Id { get; set; }

        public int BrandCode { get; set; }

        public int BranchCode { get; set; }

        public string Barcode { get; set; }

        public int Stock { get; set; }

        public string BranchName { get; set; }

        public string Article { get; set; }

        public string Name { get; set; }

        public int DiscountFee { get; set; }

        public int FullFee { get; set; }

        public int IsAvailable { get; set; }
    }
}