using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Api works";
        }
    }
}