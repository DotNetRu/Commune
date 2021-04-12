using Octokit;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// Сессия работы с файловой системой
    /// </summary>
    public class SessionContext
    {
        public GitHubClient Client { get; }

        /// <summary>
        /// исходный репозиторий, откуда был сделан форк
        /// </summary>
        public Repository OriginRepo { get; }
        public Reference OriginBranch { get; }
        public Repository LocalRepo { get; }
        public Reference CurrentBranch { get; }
    }
}
