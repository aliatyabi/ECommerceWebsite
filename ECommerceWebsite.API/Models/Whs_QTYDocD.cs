//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECommerceWebsite.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Whs_QTYDocD
    {
        public Nullable<decimal> Code { get; set; }
        public string EAN_Code { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<int> QtyR { get; set; }
        public Nullable<double> MFee { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> Fee { get; set; }
        public Nullable<double> Price { get; set; }
        public decimal ID { get; set; }
        public Nullable<int> Send { get; set; }
        public Nullable<double> TAX { get; set; }
        public Nullable<int> Issend { get; set; }
    
        public virtual Whs_QTYDocH Whs_QTYDocH { get; set; }
    }
}