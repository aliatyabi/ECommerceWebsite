﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ECommerceWebsiteEntities : DbContext
    {
        public ECommerceWebsiteEntities()
            : base("name=ECommerceWebsiteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UsersProfile> UsersProfile { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Sliders> Sliders { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Product_Categories> Product_Categories { get; set; }
        public virtual DbSet<Product_Gallery> Product_Gallery { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Product_Tags> Product_Tags { get; set; }
    }
}