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
    
    public partial class OrderByFee_Result
    {
        public int ProductId { get; set; }
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
    }
}