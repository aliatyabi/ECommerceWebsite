using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class InvoiceItemsMetadata
    {
        [Key]
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public byte Quantity { get; set; }
        public int Fee { get; set; }
        public int DiscountFee { get; set; }
        public int Price { get; set; }
    }

    [MetadataType(typeof(InvoiceItemsMetadata))]
    public partial class InvoiceItems
    { }
}