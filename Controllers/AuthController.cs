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
            var value = Request.Headers.FirstOrDefault(h => h.Key == "auth").Value.ToString();

            if (value == string.Empty) return StatusCode(400);

            if (_auths.Contains(value).ToString() != string.Empty)
            {
                _auths.Add(value);
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
            var value = Request.Headers.FirstOrDefault(h => h.Key == "auth").Value.ToString();
            
            if (value == string.Empty) return StatusCode(401);

            return StatusCode(_auths.Contains(value) ? 200 : 403);
        }
    }
}
