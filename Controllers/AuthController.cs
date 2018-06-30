using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentAuthorizator.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private List<string> _auths;
        public AuthController(List<string> auths)
        {
            _auths = auths;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var auth = Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
            
            if (auth == string.Empty) return StatusCode(401);

            return StatusCode(_auths.Contains(auth) ? 200 : 403);
        }

        [HttpPost]
        public IActionResult Add()
        {
            var auth = Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();

            if (auth == string.Empty) return StatusCode(400);

            if (_auths.Contains(auth))
            {
                return StatusCode(409);
            }
            else
            {
                _auths.Add(auth);
                return StatusCode(201);
            }
        }

        [HttpDelete]
        public IActionResult Delete() 
        {
            var auth= Request.Headers.FirstOrDefault(h => h.Key == "auth").Value.ToString();
            
            if (auth == string.Empty) return StatusCode(400);

            if (_auths.Contains(auth))
            {
                _auths.Remove(auth);
                return StatusCode(204);
            } 
            else
            {
                return StatusCode(404);
            }
        }
    }
}
