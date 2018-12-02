using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DevActivator.Common.BL.Config;
using DevActivator.Common.BL.Extensions;
using Microsoft.Extensions.Logging;

namespace DevActivator.Common.DAL
{
    public abstract class BaseProvider<T> where T : class, IEntity
    {
        private const string NewLineChars = "\r\n";

        private readonly XmlSerializer _xmlSerializer;
        private readonly XmlWriterSettings _xmlWriterSettings;
        private readonly XmlSerializerNamespaces _namespaces;

        private readonly Settings _settings;
        private readonly string _directory;

        protected readonly ILogger Log;

        protected BaseProvider(ILogger log, Settings settings, string directory)
        {
            Log = log;
            _settings = settings;
            _directory = directory;

            _xmlSerializer = new XmlSerializer(typeof(T));
            _xmlWriterSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                NewLineChars = NewLineChars,
                NewLineHandling = NewLineHandling.Replace
            };
            _namespaces = new XmlSerializerNamespaces();
            _namespaces.Add(string.Empty, string.Empty);
        }

        protected Task<T> GetEntityByIdAsync(string speakerId)
            => GetEntityAsync(_settings.GetEntityFilePath(_directory, speakerId));

        protected Task<T> SaveEntityAsync(T entity)
        {
            var data = Serialize(entity);

            var filePath = _settings.GetEntityFilePath(_directory, entity);
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // todo: use async Write
            File.WriteAllText(filePath, data + NewLineChars, Encoding.UTF8);

            return GetEntityAsync(filePath);
        }

        protected async Task<List<T>> GetAllAsync()
        {
            var filePaths = _settings.GetAllFilePaths(_directory);
            var entities = new List<T>(filePaths.Count);

            foreach (var filePath in filePaths.AsParallel())
            {
                var entity = await GetEntityAsync(filePath).ConfigureAwait(false);
                if (entity != null)
                {
                    entities.Add(entity);
                }
            }

            return entities.ToList();
        }

        private async Task<T> GetEntityAsync(string filePath)
        {
            var entity = default(T);

            if (File.Exists(filePath))
            {
                string data;

                using (var reader = File.OpenText(filePath))
                {
                    data = await reader.ReadToEndAsync().ConfigureAwait(false);
                }

                entity = Deserialize(data);
            }
            else
            {
                Log.LogWarning($"Not found filePath: {filePath}");
            }

            return entity;
        }

        private string Serialize(T target)
        {
            var data = new StringBuilder();

            using (var writer = XmlWriter.Create(data, _xmlWriterSettings))
            {
                _xmlSerializer.Serialize(writer, target, _namespaces);
            }

            return data.ToString();
        }

        private T Deserialize(string state)
        {
            T target;
            try
            {
                using (var reader = new StringReader(state))
                {
                    target = (T) _xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                target = default(T);
            }

            return target;
        }
    }
}