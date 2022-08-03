using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class WarehousesMetadata
    {
        [Key]
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public byte Status { get; set; }
        public int Stock1 { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }

    [MetadataType(typeof(WarehousesMetadata))]
    public partial class Warehouses
    { }
}