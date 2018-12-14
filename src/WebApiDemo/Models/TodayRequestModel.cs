using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class TodayRequestModel
    {
        public string userId { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }

        public string requestNo { get; set; }
    }
}