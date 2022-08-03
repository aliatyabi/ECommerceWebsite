using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ShoppingCart
{
    public class CheckoutViewModel
    {
        public IEnumerable<ShoppingCartViewModel> ShoppingCart { get; set; }

        public UserInformationViewModel UserInfo { get; set; }

        public byte PaymentMethod { get; set; }
    }
}
