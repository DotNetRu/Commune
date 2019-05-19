using System.Threading.Tasks;
using DotNetRuServer.Importer;
using DotNetRuServer.Meetups.DAL.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Octokit;

namespace DotNetRuServer.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class ImportController : Controller
    {
        private readonly DotNetRuServerContext _context;
        private readonly ILogger<ImportController> _logger;

        public ImportController(ILogger<ImportController> logger, DotNetRuServerContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<string> Import(string githubToken)
        {
            var github = new GitHubClient(new ProductHeaderValue("DotNetRuServer"));
            var tokenAuth = new Credentials(githubToken);
            github.Credentials = tokenAuth;

            var importer = new ImporterUtils(_context, github);

            _logger.LogInformation("Start to import Communities");
            await importer.ImportCommunities();

            _logger.LogInformation("Start to import Venues");
            await importer.ImportVenues();

            _logger.LogInformation("Start to import Friends");
            await importer.ImportFriend();

            _logger.LogInformation("Start to import Speakers");
            await importer.ImportSpeakers();

            _logger.LogInformation("Start to import Talks");
            await importer.ImportTalks();

            _logger.LogInformation("Start to import Meetups");
            await importer.ImportMeetups();

            _logger.LogInformation("All data is imported");

            return "Ok";
        }
    }
}