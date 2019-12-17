using CommandLine;

namespace DotNetRuServer.CLI
{

    /// <summary>
    /// Set of options required to export data from github Audit repo to local folder
    /// </summary>
    [Verb("exportAudit", HelpText = "Export audit db to folder.")]
    public class ExportAuditDbOptions
    {
        /// <summary>
        /// Server URL
        /// </summary>
        [Option("serverUrl", Required = true, HelpText = "Server URL. Example: https://server-dotnetru.azurewebsites.net/.")]
        public string ServerUrl { get; set; }

        /// <summary>
        /// Path for export
        /// </summary>
        [Option("localPath", Required = true, HelpText = "path in the system, where audit should be exported")]
        public string Path { get; set; }
    }
}
