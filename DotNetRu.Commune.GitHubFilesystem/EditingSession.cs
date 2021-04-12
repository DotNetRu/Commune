using Octokit;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// Сессия работы с файловой системой
    /// </summary>
    public class EditingSession
    {
        public Repository OriginRepo { get; }
        public Reference OriginBranch { get; }
        public Repository LocalRepo { get; }
        public Reference CurrentBranch { get; }

    }
}
