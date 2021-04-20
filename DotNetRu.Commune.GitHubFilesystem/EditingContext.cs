using System.Threading.Tasks;
using Octokit;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// Сессия работы с файловой системой
    /// </summary>
    public class EditingContext
    {
        /// <summary>
        /// Клиент доступа к github
        /// </summary>
        public IGitHubClient Client { get; }

        /// <summary>
        /// Клиент доступа к контенту репозитория
        /// </summary>
        public IRepositoryContentsClient ContentClient => Client.Repository.Content;

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
        public async Task Commit()
        {

        }
    }
}
