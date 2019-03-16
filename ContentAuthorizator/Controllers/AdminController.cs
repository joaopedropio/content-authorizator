using System;
using System.Net;
using ContentAuthorizator.Helpers;
using ContentAuthorizator.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContentAuthorizator.Controllers
{
    [Route("/admin")]
    public class AdminController : Controller
    {
        private IAuthorizationRepository auths;
        private readonly ILogger<AdminController> logger;

        public AdminController(IAuthorizationRepository auths, ILogger<AdminController> logger)
        {
            this.auths = auths;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Add()
        {
            try
            {
                var auth = Domain.Authorization.Parse(Request.Body);
                auths.Store(auth);
                return new Json(HttpStatusCode.Created);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Unknow error while parsing or persisting authorization");
                return new JsonError(new { ex.Message, ex.StackTrace }, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult Index(string username)
        {
            try
            {
                var auth = auths.RetrieveByUsername(username);
                return (auth == null)
                    ? new Json(HttpStatusCode.NotFound)
                    : new Json(auth, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while retrieving {username} user.", username);
                return new JsonError(new { ex.Message, ex.StackTrace }, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{username}")]
        public IActionResult Delete(string username)
        {
            try
            {
                var auth = auths.RetrieveByUsername(username);
                auths.Remove(auth);
                return new Json(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error while retrieving or removing authorization from {username} user.", username);
                return new JsonError(new { ex.Message, ex.StackTrace }, HttpStatusCode.InternalServerError);
            }
        }
    }
}
