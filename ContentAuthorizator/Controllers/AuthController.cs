using ContentAuthorizator.Helpers;
using ContentAuthorizator.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ContentAuthorizator.Controllers
{
    [Route("/auth")]
    public class AuthController : Controller
    {
        private IAuthorizationRepository auths;
        private readonly ILogger<AuthController> logger;

        public AuthController(IAuthorizationRepository auths, ILogger<AuthController> logger)
        {
            this.auths = auths;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
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
