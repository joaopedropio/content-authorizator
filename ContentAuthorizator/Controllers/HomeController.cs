using Microsoft.AspNetCore.Mvc;

namespace ContentAuthorizator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "OK";
        }
    }
}
