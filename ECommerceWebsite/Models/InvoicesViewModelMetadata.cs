using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class InvoicesViewModelMetadata
    {
        [Key]
        public int Id { get; set; }
        public Invoices Invoice { get; set; }

        public IEnumerable<InvoiceItems> InvoiceItems { get; set; }
    }

    [MetadataType(typeof(InvoicesViewModelMetadata))]
    public partial class InvoicesViewModel
    { }
}