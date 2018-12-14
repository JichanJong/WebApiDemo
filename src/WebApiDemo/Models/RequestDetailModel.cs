using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class RequestDetailModel
    {
            public string userId { get; set; }
            public string requestTableNo { get; set; }
            public string departmentId { get; set; }
            public string salesTableNo { get; set; }
            public int pageIndex { get; set; }
            public int pageSize { get; set; }

    }
}