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
    
    public partial class Product_Sizes
    {
        public Product_Sizes()
        {
            this.InvoiceItems = new HashSet<InvoiceItems>();
            this.Stock = new HashSet<Stock>();
        }
    
        public int SizeId { get; set; }
        public string SizeUK { get; set; }
        public string SizeIR { get; set; }
        public int SizeGroup { get; set; }
    
        public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }
        public virtual SizeGroups SizeGroups { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
    }
}
