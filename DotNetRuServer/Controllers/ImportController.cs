using System;
using System.Threading.Tasks;
using DotNetRuServer.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class ImportController : BaseController
    {
        private readonly IImporter _importer;

        protected ImportController() { }

        public ImportController(IImporter importer, ILoggerFactory logger) : base(logger)
        {
            _importer = importer;
        }


        [HttpPost]
        public Task<string> Import(string githubToken)
        {
            try
            {
                LogMethodBegin(githubToken);

                Task<string> result = _importer.Import(githubToken);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }
    }
}