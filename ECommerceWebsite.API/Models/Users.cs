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
    
    public partial class Users
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> PermissionLevel { get; set; }
        public string Location { get; set; }
        public byte[] Pic { get; set; }
        public Nullable<int> Lock { get; set; }
        public Nullable<int> Service { get; set; }
        public Nullable<int> Send { get; set; }
    }
}
