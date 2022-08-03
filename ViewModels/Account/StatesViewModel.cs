using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels.Account
{
    public class StatesViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "StateRequired")]
        public int StateId { get; set; }

        public string StateName { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }
    }
}
