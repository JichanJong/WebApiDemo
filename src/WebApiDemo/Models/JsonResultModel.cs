using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class JsonResultModel
    {
        public bool success { get; set; }

        public string msg { get; set; }

        public object data { get; set; }
    }
}