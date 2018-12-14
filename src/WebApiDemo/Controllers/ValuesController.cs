using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IEnumerable<string> Post()
        {
            var request = HttpContext.Current.Request;
            string data = request.Form["data"];
            string factory = request.Form["factory_code"];
            string vender = request.Form["vender_code"];
            return new[] { factory, vender, data };
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        //[HttpPost]
        //public string PostData()
        //{
        //    return "true";
        //}
        [HttpPost]
        public object GetList()
        {
            var request = HttpContext.Current.Request;
            using (StreamReader reader = new StreamReader(request.InputStream))
            {
                string json = reader.ReadToEnd();
                PageModel model = JsonConvert.DeserializeObject<PageModel>(json);
                if (model == null)
                {
                    return null;
                }

                if (model.pageIndex < 1)
                {
                    model.pageIndex = 1;
                }

                if (model.pageSize < 1)
                {
                    model.pageSize = 10;
                }

                List<Person> lstData = new List<Person>();
                for (int i = 0; i < 100; i++)
                {
                    lstData.Add(new Person { id = i, age = i + 18 % 60, name = $"A{i}Bcd{i % 10}" });
                }

                var data = lstData.Skip((model.pageIndex - 1) * model.pageSize).Take(model.pageSize).ToArray();
                return new { recordCount = lstData.Count, data = data };
            }
        }

        [HttpPost]
        public object getStorageByNo()
        {
            string json = GetJson();
            NoModel model = JsonConvert.DeserializeObject<NoModel>(json);
            EntityJsonModel info = new EntityJsonModel
            {
                id = GuidString,
                no = model.no,
                name = "储位1"
            };
            return new JsonResultModel { success = true, data = info };
        }

        [NonAction]
        private string GetJson()
        {
            var request = HttpContext.Current.Request;
            using (StreamReader reader = new StreamReader(request.InputStream))
            {
                return reader.ReadToEnd();
            }
        }

        [HttpPost]
        public object saveStorageAndPackageList()
        {
            string json = GetJson();
            StorageAndPackageModel model = JsonConvert.DeserializeObject<StorageAndPackageModel>(json);
            return new JsonResultModel { success = true };
        }

        private string[] departments = { "财务", "售后", "行政", "业务" };

        [HttpPost]
        public object getTodayRequestList()
        {
            string json = GetJson();
            TodayRequestModel model = JsonConvert.DeserializeObject<TodayRequestModel>(json);
            List<object> lstData = new List<object>();
            for (int i = 0; i < 50; i++)
            {
                lstData.Add(new { requestTableId = GuidString, requestTableNo = $"abcd{i}", departmentName = departments[i % 4], requestIssueDate = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss") });
            }

            var datas = lstData.Skip((model.pageIndex - 1) * model.pageSize).Take(model.pageSize).ToArray();
            return new JsonResultModel() { success = true, data = new { recordCount = lstData.Count, list = datas } };
        }

        [HttpPost]
        public object getStoragePackageLineList()
        {
            string json = GetJson();
            StoragePackageModel model = JsonConvert.DeserializeObject<StoragePackageModel>(json);
            List<object> lstData = new List<object>();
            for (int i = 0; i < 5; i++)
            {
                lstData.Add(new { requestTableId = GuidString, requestTableNo = $"abcd{i}", storageId = GuidString, storageName = $"储位{i}", packageId = GuidString, packageName = $"包装{i % 7}" });
            }
            return new JsonResultModel() { success = true, data = lstData };
        }
        [HttpPost]
        public object getRequestLineList()
        {
            string json = GetJson();
            RequestLineByPackageAndStorage model = JsonConvert.DeserializeObject<RequestLineByPackageAndStorage>(json);
            List<object> lstData = new List<object>();
            for (int i = 0; i < 20; i++)
            {
                lstData.Add(new { salesLineId = GuidString, salesTableNo = $"hijk{i}", articlePartId = GuidString, articlePartName = $"部位{i}", size = $"{i % 7 + 6}", pendingIssueQty = i % 24 + 10 });
            }
            return new JsonResultModel() { success = true, data = lstData };
        }

        [HttpPost]
        public object saveStorageOutList()
        {
            string json = GetJson();
            SaveStorageOutListModel model = JsonConvert.DeserializeObject<SaveStorageOutListModel>(json);
            return new JsonResultModel() { success = true };
        }
        [HttpPost]
        public object getDepartmentList()
        {
            string json = GetJson();
            BaseJsonModel model = JsonConvert.DeserializeObject<BaseJsonModel>(json);
            List<object> lstData = new List<object>();
            for (int i = 0; i < 5; i++)
            {
                lstData.Add(new
                {
                    id = GuidString,
                    no = $"EI18391{i}",
                    name = departments[i % departments.Length]
                });
            }
            return new JsonResultModel() { success = true, data = lstData };
        }

        [HttpPost]
        public object getRequestDetailList()
        {
            string json = GetJson();
            RequestDetailModel model = JsonConvert.DeserializeObject<RequestDetailModel>(json);
            List<object> lstData = new List<object>();
            for (int i = 0; i < 50; i++)
            {
                List<object> details = new List<object>();
                for (int j = 0; j < 3; j++)
                {
                    details.Add(new { storageNo = $"储位{j}", packageNo = $"包装XI{j * 2}", stockQty = i * (j + 1) / 3 + 5 });
                }
                lstData.Add(new
                {
                    requestTableId = GuidString,
                    requestTableNo = $"abcd{i}",
                    requestDepartment = departments[i % 4],
                    salesTableNo = $"NACD180012345789{i}",
                    articlePartName = $"部位{i * 2}",
                    size = $"{i % 7 + 6}",
                    requestQty = i + 20,
                    unIssueQty = i % 5 + 12,
                    stockQty = i + 15,
                    requiredIssueDate = DateTime.Now.AddHours(-i).ToString("yyyy-MM-dd HH:mm:ss"),
                    stockList = details              
                });
            }

            var datas = lstData.Skip((model.pageIndex - 1) * model.pageSize).Take(model.pageSize).ToArray();
            return new JsonResultModel() { success = true, data = new { recordCount = lstData.Count, list = datas } };
        }

        public static string GuidString => Guid.NewGuid().ToString("N");
    }
}
