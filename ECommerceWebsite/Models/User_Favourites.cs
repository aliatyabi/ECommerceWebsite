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
    
    public partial class User_Favourites
    {
        public int WishId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual Users Users { get; set; }
    }
}
