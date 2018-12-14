using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class PageModel
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}