using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Controllers
{
    public class HomeController : BaseController
    {
        protected HomeController() { }

        public HomeController(ILoggerFactory logger) : base(logger)
        {
        }
    }
}