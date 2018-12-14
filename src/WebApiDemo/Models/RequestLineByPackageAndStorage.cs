using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class RequestLineByPackageAndStorage
    {
        public string userId { get; set; }

        public string requestTableId { get; set; }

        public string storageId { get; set; }
        public string packageId { get; set; }

    }
}