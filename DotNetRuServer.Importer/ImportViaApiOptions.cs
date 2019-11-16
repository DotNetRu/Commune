using CommandLine;

namespace DotNetRuServer.Importer
{
    /// <summary>
    /// Set of options required to import data from github Audit repo to
    /// Server via API
    /// </summary>
    [Verb("importViaApi", HelpText = "Imports Audit data from Github repo to the Server using API.")]
    public class ImportViaApiOptions
    {
        /// <summary>
        /// Server URL
        /// </summary>
        [Option("serverUrl", Required = true, HelpText = "Server URL. Example: https://server-dotnetru.azurewebsites.net/.")]
        public string ServerUrl { get; set; }

        /// <summary>
        /// Github token
        /// </summary>
        [Option("githubToken", Required = true, HelpText = "Github token.")]
        public string GithubToken { get; set; }
    }
}
