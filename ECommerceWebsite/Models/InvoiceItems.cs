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
    
    public partial class InvoiceItems
    {
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public byte Quantity { get; set; }
        public int Fee { get; set; }
        public int DiscountFee { get; set; }
        public Nullable<int> Price { get; set; }
    
        public virtual Invoices Invoices { get; set; }
        public virtual Product_Sizes Product_Sizes { get; set; }
        public virtual Products Products { get; set; }
    }
}
