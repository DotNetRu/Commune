using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class ImportController : Controller
    {
        private readonly Application.Importer _importer;

        public ImportController(Application.Importer importer)
        {
            _importer = importer;
        }


        [HttpPost]
        public Task<string> Import(string githubToken)
        {
            return _importer.Import(githubToken);
        }
    }
}