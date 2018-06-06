using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

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
            return new[] { factory,vender,data };
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
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
    }
}
