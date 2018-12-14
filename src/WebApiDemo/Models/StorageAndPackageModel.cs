using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class StorageAndPackageModel
    {
        public string userId { get; set; }

        public List<StorageAndPackageItem> data { get; set; };
    }

    public class StorageAndPackageItem
    {
        public string storageId { get; set; }

        public string packageId { get; set; }
    }
}