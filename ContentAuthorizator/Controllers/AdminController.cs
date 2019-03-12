using System;
using System.Net;
using ContentAuthorizator.Helpers;
using ContentAuthorizator.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ContentAuthorizator.Controllers
{
    [Route("/admin")]
    public class AdminController : Controller
    {
        private IAuthorizationRepository auths;
        public AdminController(IAuthorizationRepository auths)
        {
            this.auths = auths;
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
                return new JsonError(new { ex.Message, ex.StackTrace }, HttpStatusCode.InternalServerError);
            }
        }
    }
}
