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

        [Route("[controller]/[action]")]
        public IActionResult AddAuth()
        {
            var header = Request.Headers.First(h => h.Key == "auth");

            if (header.Value == "") return StatusCode(404);

            if (!_auths.Contains(header.Value))
            {
                _auths.Add(header.Value);
                return StatusCode(200);
            }
            else
            {
                return StatusCode(409);
            }
        }

        [Route("[controller]")]
        public IActionResult Index()
        {
            var header = Request.Headers.First(h => h.Key == "auth");
            
            if (header.Value == string.Empty) return StatusCode(404);

            return StatusCode(_auths.Contains(header.Value) ? 200 : 403);
        }
    }
}
