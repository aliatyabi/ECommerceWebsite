using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class InvoicesMetadata
    {
        [Key]
        [Display(Name = "InvoiceId", ResourceType = typeof(Resources.Resource))]
        public int InvoiceId { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Resources.Resource))]
        public short TransactionType { get; set; }

        [Display(Name = "InvoiceDate", ResourceType = typeof(Resources.Resource))]
        public System.DateTime InvoiceDate { get; set; }

        [Display(Name = "User", ResourceType = typeof(Resources.Resource))]
        public int UserId { get; set; }

        [Display(Name = "InvoiceAmount", ResourceType = typeof(Resources.Resource))]
        public int InvoiceAmount { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(Resources.Resource))]
        public int Discount { get; set; }

        [Display(Name = "InvoiceNetAmount", ResourceType = typeof(Resources.Resource))]
        public int InvoiceNetAmount { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(Resources.Resource))]
        public int Quantity { get; set; }
    }

    [MetadataType(typeof(InvoicesMetadata))]
    public partial class Invoices
    { }
}