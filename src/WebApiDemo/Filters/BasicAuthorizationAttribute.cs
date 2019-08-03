using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiDemo.Filters
{
    public class BasicAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var auth = actionContext.Request.Headers.Authorization;
            if (auth == null || auth.Scheme != "Basic")
            {
                ResponseUnauthorized(actionContext);
            }
            else
            {
                string tokenString = auth.Parameter;
                if (string.IsNullOrEmpty(tokenString))
                {
                    ResponseUnauthorized(actionContext);
                }
                else
                {
                    string[] tokens = Encoding.UTF8.GetString(Convert.FromBase64String(tokenString)).Split(':');
                    if (tokens.Length < 2)
                    {
                        ResponseUnauthorized(actionContext);
                    }
                    else
                    {
                        string username = tokens[0];
                        string password = tokens[1];
                        bool valid = ValidateUser(username, password);
                        if (valid)
                        {
                            IIdentity identity = new GenericIdentity(username);
                            Thread.CurrentPrincipal = new GenericPrincipal(identity, null);
                        }
                        else
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                            ApiResponseModel model = new ApiResponseModel {Status = 0, Message = "用户名或密码不正确"};
                            actionContext.Response.Content =
                                new ObjectContent<ApiResponseModel>(model, new JsonMediaTypeFormatter());
                        }
                    }
                }
            }
            base.OnAuthorization(actionContext);
        }

        private bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            return username == "admin" && password == "123";
        }

        private static void ResponseUnauthorized(HttpActionContext actionContext)
        {
            string host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{host}\"");
        }
    }

    internal class ApiResponseModel
    {
        public int Status { get; set; }

        public string Message { get; set; }
    }
}