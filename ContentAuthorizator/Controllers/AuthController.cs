using ContentAuthorizator.Helpers;
using ContentAuthorizator.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContentAuthorizator.Controllers
{
    [Route("/auth")]
    public class AuthController : Controller
    {
        private IAuthorizationRepository auths;
        public AuthController(IAuthorizationRepository auths)
        {
            this.auths = auths;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HttpRequestHelper.PrintHeaders(HttpContext);
            if (!HttpRequestHelper.IsRequestValid(Request))
                return new Json(HttpStatusCode.Unauthorized);

            var ipAddress = HttpRequestHelper.GetIPAdress(Request);
            var authorization = auths.RetrieveByIpAddress(ipAddress);

            if (ipAddress == null || !ipAddress.Equals(authorization?.IpAdress))
                return new Json(HttpStatusCode.Forbidden);

            return new Json(HttpStatusCode.OK);
        }
    }
}
