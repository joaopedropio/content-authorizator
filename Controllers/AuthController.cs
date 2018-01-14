using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentAuthorizator.Controllers
{
    public class AuthController : Controller
    {
        private List<string> _auths;
        public AuthController(List<string> auths)
        {
            _auths = auths;
        }

        [HttpGet]
        [Route("[controller]")]
        public IActionResult Index()
        {
            var authorization = Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
            
            if (authorization == string.Empty) return StatusCode(401);

            return StatusCode(_auths.Contains(authorization) ? 200 : 403);
        }

        [HttpPost]
        [Route("[controller]")]
        public IActionResult Add()
        {
            var authorization = Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();

            if (authorization == string.Empty) return StatusCode(400);

            if (_auths.Contains(authorization))
            {
                return StatusCode(409);
            }
            else
            {
                _auths.Add(authorization);
                return StatusCode(201);
            }
        }

        [HttpDelete]
        [Route("[controller]")]
        public IActionResult Delete() 
        {
            var authorization = Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
            
            if (authorization == string.Empty) return StatusCode(400);

            if (_auths.Contains(authorization))
            {
                _auths.Remove(authorization);
                return StatusCode(204);
            } 
            else
            {
                return StatusCode(404);
            }
        }
    }
}
