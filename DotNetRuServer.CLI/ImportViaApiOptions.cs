using CommandLine;

namespace DotNetRuServer.CLI
{
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
