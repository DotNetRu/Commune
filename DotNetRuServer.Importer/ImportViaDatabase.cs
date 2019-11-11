using CommandLine;

namespace DotNetRuServer.Importer
{
    /// <summary>
    /// Set of options required to import data from github Audit repo to
    /// Server via API
    /// </summary>
    [Verb("importViaDatabase", HelpText = "Imports Audit data from Github repo to the Server using direct database connection.")]
    public class ImportViaDatabaseOptions
    {
        /// <summary>
        /// Server URL
        /// </summary>
        [Option("dbConnection", Required = true, HelpText = "Database connection string.")]
        public string DatabaseConnection { get; set; }

        /// <summary>
        /// Github token
        /// </summary>
        [Option("githubToken", Required = true, HelpText = "Github token.")]
        public string GithubToken { get; set; }
    }
}
