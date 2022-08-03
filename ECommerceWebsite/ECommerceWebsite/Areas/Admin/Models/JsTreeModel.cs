using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceWebsite.Areas.Admin.Models
{
    public class JsTreeModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public JsTreeNodeState state { get; set; }
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
        public string li_attr { get; set; }
        public string a_attr { get; set; }
    }

    public class JsTreeNodeState
    {
        public bool opened { set; get; }
        public bool disabled { set; get; }
        public bool selected { set; get; }

        public JsTreeNodeState()
        {
            opened = true;
        }
    }
}