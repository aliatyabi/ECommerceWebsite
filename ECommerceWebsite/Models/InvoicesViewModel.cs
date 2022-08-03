using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Models
{
    public class InvoicesViewModel
    {
        public Invoices Invoice { get; set; }

        public List<InvoiceItems> InvoiceItems { get; set; }
    }
}