using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevActivator.Common.BL.Config;

namespace DevActivator.Common.BL.Extensions
{
    // todo: move from BL
    public static class SettingsExtensions
    {
        private const string IndexFileName = "index.xml";
        private const string AvatarFileName = "avatar.jpg";

        private const string Db = "db";

        public static string GetSpeakerAvatarFilePath(this Settings settings, string speakerId)
            => Path.Combine(settings.GetDirectory("speakers"), speakerId, AvatarFileName);

        public static string GetSpeakerFilePath(this Settings settings, string speakerId)
            => Path.Combine(settings.GetDirectory("speakers"), speakerId, IndexFileName);

        public static List<string> GetAllFilePaths(this Settings settings, string directoryName, bool flatEntity)
        {
            var entityDirectory = settings.GetDirectory(directoryName);
            if (!flatEntity)
            {
                return Directory.GetDirectories(entityDirectory)
                    .Select(directory => Path.Combine(directory, IndexFileName)).ToList();
            }

            return Directory.GetFiles(entityDirectory, "*.xml").ToList();
        }

        public static string GetEntityFilePath(this Settings settings, string directoryName, IEntity entity,
            bool flatEntity)
            => settings.GetEntityFilePath(directoryName, entity.Id, flatEntity);

        public static string GetEntityFilePath(this Settings settings, string directoryName, string entityId,
            bool flatEntity)
        {
            if (!flatEntity)
            {
                return Path.Combine(settings.GetDirectory(directoryName), entityId, IndexFileName);
            }

            return Path.Combine(settings.GetDirectory(directoryName), $"{entityId}.xml");
        }

        private static string GetDirectory(this Settings settings, string directoryName)
            => Path.Combine(settings.AuditRepoDirectory, Db, directoryName);
    }
}