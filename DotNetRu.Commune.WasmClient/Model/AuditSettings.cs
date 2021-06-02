namespace DotNetRu.Commune.WasmClient.Model
{
    /// <summary>
    /// Класс настроек расположения репозитория аудита
    /// </summary>
    public class AuditSettings
    {
        /// <summary>
        /// Наименование репозитория
        /// </summary>
        public string RepositoryName { get; set; } = string.Empty;

        /// <summary>
        /// Наименование оригинального владельца репозитория
        /// </summary>
        public string OriginalOwner { get; set; } = string.Empty;
    }
}
