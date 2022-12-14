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
    
    public partial class Users
    {
        public Users()
        {
            this.Invoices = new HashSet<Invoices>();
            this.Product_Reviews = new HashSet<Product_Reviews>();
            this.User_Favourites = new HashSet<User_Favourites>();
            this.UsersInformation = new HashSet<UsersInformation>();
        }
    
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActivationCode { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime RegisterationDate { get; set; }
    
        public virtual ICollection<Invoices> Invoices { get; set; }
        public virtual ICollection<Product_Reviews> Product_Reviews { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual ICollection<User_Favourites> User_Favourites { get; set; }
        public virtual ICollection<UsersInformation> UsersInformation { get; set; }
    }
}
