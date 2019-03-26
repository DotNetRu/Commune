using Microsoft.AspNetCore.Mvc;

namespace DevActivator.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Api works";
        }
    }
}