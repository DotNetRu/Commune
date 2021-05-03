using System;
using System.Threading.Tasks;
using Octokit;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// Сессия работы с файловой системой
    /// </summary>
    internal class EditingContext
    {
        /// <summary>
        /// Клиент доступа к github
        /// </summary>
        public IGitHubClient Client { get; }

        /// <summary>
        /// Клиент доступа к контенту репозитория
        /// </summary>
        public virtual IRepositoryContentsClient ContentClient => Client.Repository.Content;

        /// <summary>
        /// Клиент доступа к списку pull request
        /// </summary>
        public virtual IPullRequestsClient PullRequestsClient => Client.PullRequest;

        /// <summary>
        /// исходный репозиторий, откуда был сделан форк
        /// </summary>
        public Repository OriginRepo { get; }

        /// <summary>
        /// Исходная ветвь. Туда будет сделан PR призавершении контекста
        /// </summary>
        public Reference OriginBranch { get; }

        /// <summary>
        /// Локальный форк репозитория в репозиториях пользователя
        /// </summary>
        public Repository LocalRepo { get; }

        /// <summary>
        /// Текущая ветка, в которой будут производится манипуляции с файлами
        /// </summary>
        public Reference CurrentBranch { get; }

        /// <summary>
        /// Применить изменения, создав PR в основную ветвь основного репозитория
        /// </summary>
        public Task Commit() =>
            PullRequestsClient.Create(OriginRepo.Id,
                new("AUTOMATED PR", CurrentBranch.Ref, OriginBranch.Ref) {Draft = true});

        public EditingContext(IGitHubClient client, Repository originRepo, Reference originBranch, Repository localRepo,
            Reference currentBranch)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            OriginRepo = originRepo ?? throw new ArgumentNullException(nameof(originRepo));
            OriginBranch = originBranch ?? throw new ArgumentNullException(nameof(originBranch));
            LocalRepo = localRepo ?? throw new ArgumentNullException(nameof(localRepo));
            CurrentBranch = currentBranch ?? throw new ArgumentNullException(nameof(currentBranch));
        }
    }
}
