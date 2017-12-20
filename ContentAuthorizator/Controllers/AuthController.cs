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
        public IActionResult Add()
        {
            var header = Request.Headers.FirstOrDefault(h => h.Key == "auth");

            if (header.Value.ToString() == "") return StatusCode(401);

            if (_auths.Contains(header.Value).ToString() != string.Empty)
            {
                _auths.Add(header.Value);
                return StatusCode(200);
            }
            else
            {
                return StatusCode(403);
            }
        }

        [Route("[controller]")]
        public IActionResult Index()
        {
            var header = Request.Headers.FirstOrDefault(h => h.Key == "auth");
            
            if (header.Value.ToString() == string.Empty) return StatusCode(401);

            return StatusCode(_auths.Contains(header.Value) ? 200 : 403);
        }
    }
}
