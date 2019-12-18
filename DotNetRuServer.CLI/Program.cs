using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;

namespace DotNetRuServer.CLI
{
    class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<ExportAuditDbOptions, ImportViaApiOptions>(args);

            return await options.MapResult(
                async (ImportViaApiOptions o) => await ImportViaApi(o),
                async (ExportAuditDbOptions o) => await ExportAuditDb(o),
                _ => Task.FromResult(-1));
        }

        private static async Task<int> ImportViaApi(ImportViaApiOptions options)
        {
            Console.WriteLine("Importing data to Server at {0} from Github.", options.ServerUrl);

            var query = $"{options.ServerUrl}api/import?githubToken={options.GithubToken}";

            var client = new HttpClient();
            var result = await client.PostAsync(query, new StringContent(string.Empty));

            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("Completed successfully!");
                return 0;
            }

            Console.WriteLine("Import failed.");
            Console.WriteLine(await result.Content.ReadAsStringAsync());
            return -1;
        }

        private static async Task<int> ExportAuditDb(ExportAuditDbOptions options)
        {
            Console.WriteLine($"Export data from Server at {options.ServerUrl} to folder {options.Path}");

            var query = $"{options.ServerUrl}api/export";
            var client = new HttpClient();
            var result = await client.PostAsync(query, new StringContent(string.Empty));

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Export failed.");
                return -1;
            }

            Console.WriteLine($"Export data to {options.Path}");
            var zipFolder = Directory.CreateDirectory(Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid().ToString()
            ));
            var zipPath = Path.Combine(zipFolder.FullName, "audit.zip");

            await using(var stream = await result.Content.ReadAsStreamAsync())
            {
                await using var zip = File.OpenWrite(zipPath);
                stream.CopyTo(zip);
            }

            ZipFile.ExtractToDirectory(zipPath, options.Path, true);
            zipFolder.Delete(true);

            Console.WriteLine("Completed successfully!");
            return 0;
        }
    }
}
