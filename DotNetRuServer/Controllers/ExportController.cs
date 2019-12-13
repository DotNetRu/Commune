using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using DotNetRuServer.Application;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
{
    [ApiController]
    [Route("api/export")]
    public class ExportController : Controller
    {
        private readonly IExporter _exporter;

        public ExportController(IExporter exporter)
        {
            _exporter = exporter;
        }

        [HttpPost]
        public async Task<IActionResult> Export()
        {
            var info = await _exporter.Export();
            string zipFile = Path.Combine(info.FullName, "Audit.zip");
            ZipFile.CreateFromDirectory(
                Path.Combine(info.FullName, "Audit"),
                zipFile
                );
            
            return File(new FileStream(zipFile, FileMode.Open), "application/zip", "Audit.zip");
        }
    }
}
