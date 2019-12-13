using System;
using System.IO;
using System.Threading.Tasks;
using DotNetRuServer.Exporter;
using DotNetRuServer.Meetups.DAL.Database;
using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Application
{
    public interface IExporter
    {
        Task<DirectoryInfo> Export();
    }

    public class Exporter : IExporter
    {
        private readonly DotNetRuServerContext _context;
        private readonly ILogger<Importer> _logger;

        public Exporter(DotNetRuServerContext context, ILogger<Importer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DirectoryInfo> Export()
        {
            var exportFolder = Directory.CreateDirectory(Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid().ToString()
                ));
            var auditFolder = exportFolder.CreateSubdirectory(Path.Combine("Audit","db"));

            var export = new ExporterUtils(_context, auditFolder);

            Console.WriteLine("Starting export Communities");
            await export.ExportCommunties();

            Console.WriteLine("Starting export Friends");
            await export.ExportFriends();

            Console.WriteLine("Starting export Meetups");
            await export.ExportMeetups();

            Console.WriteLine("Starting export Spekers");
            await export.ExportSpeekers();

            Console.WriteLine("Starting export Talks");
            await export.ExportTalks();

            Console.WriteLine("Starting export Meetups");
            await export.ExportVenues();

            return exportFolder;
        }
    }

}
