using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Invoice
{
    public class InvoiceItemsViewModel
    {
        public int Id { get; set; }

        [Key]
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "QuantityRequired")]
        [Display(Name = "Quantity", ResourceType = typeof(Resources.Resource))]
        [Range(1, byte.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ValidByte")]
        public byte Quantity { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FeeRequired")]
        [Display(Name = "Fee", ResourceType = typeof(Resources.Resource))]
        [Range(1000, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ValidInt")]
        public int Fee { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DiscountFeeRequired")]
        [Display(Name = "DiscountFee", ResourceType = typeof(Resources.Resource))]
        [Range(1000, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ValidInt")]
        public int DiscountFee { get; set; }
        public Nullable<int> Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProductRequired")]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ImageName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SizeRequired")]
        public int SizeId { get; set; }

        public string Size { get; set; }

        public Int16 TransactionType { get; set; }
    }
}
