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
    
    public partial class Invoices
    {
        public Invoices()
        {
            this.InvoiceItems = new HashSet<InvoiceItems>();
        }
    
        public int InvoiceId { get; set; }
        public int Quantity { get; set; }
        public short TransactionType { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public int UserId { get; set; }
        public int InvoiceAmount { get; set; }
        public int Discount { get; set; }
        public int InvoiceNetAmount { get; set; }
        public bool Completed { get; set; }
        public Nullable<long> TrackingCode { get; set; }
        public string Status { get; set; }
        public Nullable<long> Authority { get; set; }
    
        public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }
        public virtual Users Users { get; set; }
    }
}