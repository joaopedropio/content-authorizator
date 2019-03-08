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
        public AuthController(IAuthorizationRepository auths)
        {
            this.authorizator = new Authorizator(auths);
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!HttpRequestHelper.IsRequestValid(Request))
                return StatusCode(HttpStatusCode.Unauthorized.GetHashCode());

            var authorization = HttpRequestHelper.GetAuthorization(Request);
            
            return StatusCode(authorizator.IsAuthorizationValid(authorization)
                ? HttpStatusCode.OK.GetHashCode()
                : HttpStatusCode.Forbidden.GetHashCode());
        }

        [HttpPost]
        public IActionResult Add()
        {
            if (!HttpRequestHelper.IsRequestValid(Request))
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode());

            var authorization = HttpRequestHelper.GetAuthorization(Request);
            authorizator.Auths.Store(authorization);

            return StatusCode(HttpStatusCode.OK.GetHashCode());
        }

        [HttpDelete]
        public IActionResult Delete() 
        {
            if (!HttpRequestHelper.IsRequestValid(Request))
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode());


            var authorization = HttpRequestHelper.GetAuthorization(Request);

            if (authorizator.IsAuthorizationValid(authorization))
            {
                authorizator.Auths.Remove(authorization);
                return StatusCode(HttpStatusCode.NoContent.GetHashCode());
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound.GetHashCode());
            }
        }
    }
}
