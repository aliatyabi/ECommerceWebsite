//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECommerceWebsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int WarehouseId { get; set; }
        public int Stock1 { get; set; }
    
        public virtual Product_Sizes Product_Sizes { get; set; }
        public virtual Products Products { get; set; }
        public virtual Warehouses Warehouses { get; set; }
    }
}