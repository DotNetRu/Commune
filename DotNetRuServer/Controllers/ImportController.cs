using System.Threading.Tasks;
using DotNetRuServer.Application;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class ImportController : Controller
    {
        private readonly IImporter _importer;

        public ImportController(IImporter importer)
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