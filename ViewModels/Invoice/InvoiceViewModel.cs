using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Invoice
{
    public class InvoiceViewModel
    {
        [Key]
        [Display(Name = "InvoiceId", ResourceType = typeof(Resources.Resource))]
        public int InvoiceId { get; set; }

        [Display(Name = "InvoiceNumber", ResourceType = typeof(Resources.Resource))]
        public int InvoiceNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TransactionTypeRequired")]
        [Display(Name = "TransactionType", ResourceType = typeof(Resources.Resource))]
        public short TransactionType { get; set; }

        [Display(Name = "InvoiceDate", ResourceType = typeof(Resources.Resource))]
        public System.DateTime InvoiceDate { get; set; }

        [Display(Name = "UserId", ResourceType = typeof(Resources.Resource))]
        public int UserId { get; set; }

        [Display(Name = "InvoiceAmount", ResourceType = typeof(Resources.Resource))]
        public int InvoiceAmount { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(Resources.Resource))]
        public int Discount { get; set; }

        [Display(Name = "InvoiceNetAmount", ResourceType = typeof(Resources.Resource))]
        public int InvoiceNetAmount { get; set; }
    }
}
