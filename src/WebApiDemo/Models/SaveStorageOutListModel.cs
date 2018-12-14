using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{

        public class SaveStorageOutListModel
        {
            public string userId { get; set; }
            public string requestTableId { get; set; }
            public string storageId { get; set; }
            public string packageId { get; set; }
            public Datum[] data { get; set; }
        }

        public class Datum
        {
            public string salesLineId { get; set; }
            public string articleId { get; set; }
            public int issueQty { get; set; }
        }
}