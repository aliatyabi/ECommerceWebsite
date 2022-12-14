using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Product
{
    public class WishListViewModel
    {
        public string ImageName { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }
    }
}
