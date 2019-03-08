using ContentAuthorizator.Domain;
using ContentAuthorizator.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContentAuthorizator.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private IAuthorizator authorizator;
        public AuthController(IAuthorizator authorizator)
        {
            this.authorizator = authorizator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!HttpRequestHelper.IsRequestValid(Request))
                return StatusCode(HttpStatusCode.Unauthorized.GetHashCode());

            var authorization = HttpRequestHelper.GetAuthorization(Request);
            
            return StatusCode(authorizator.IsAuthorizationValid(authorization) ? HttpStatusCode.OK.GetHashCode() : HttpStatusCode.Forbidden.GetHashCode());
        }

        [HttpPost]
        public IActionResult Add()
        {
            //var auth = Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();

            //if (auth == string.Empty) return StatusCode(400);

            //if (_auths.Contains(auth))
            //{
            //    return StatusCode(409);
            //}
            //else
            //{
            //    _auths.Add(auth);
                return StatusCode(201);
            //}
        }

        [HttpDelete]
        public IActionResult Delete() 
        {
            //var auth= Request.Headers.FirstOrDefault(h => h.Key == "auth").Value.ToString();
            
            //if (auth == string.Empty) return StatusCode(400);

            //if (_auths.Contains(auth))
            //{
            //    _auths.Remove(auth);
            //    return StatusCode(204);
            //} 
            //else
            //{
                return StatusCode(404);
            //}
        }
    }
}
