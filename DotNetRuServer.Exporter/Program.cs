using System;
using DotNetRuServer.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Extensions;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Exporter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = args[0];
            var rootDirectoryPath = args[1];

            var optionsBuilder = new DbContextOptionsBuilder<DotNetRuServerContext>();

            optionsBuilder.UseSqlServer(connectionString);
            var context = new DotNetRuServerContext(optionsBuilder.Options);

            var directoryInfo = new DirectoryInfo(rootDirectoryPath);
            var dbDirectory = directoryInfo.CreateSubdirectory(@"Audit\db");

            var export = new ExporterUtils(context, dbDirectory);

            Console.WriteLine("Starting export Communities");
            await export.ExportCommunties();

            Console.WriteLine("Starting export Friends");
            await export.ExportFriends();

            Console.WriteLine("Starting export Meetups");
            await export.ExportMeetups();  

            Console.WriteLine("Starting export Spekers");
            await export.ExportSpeekers();

            Console.WriteLine("Starting export Talks");
            await export.ExportTalks();

            Console.WriteLine("Starting export Meetups");
            await export.ExportVenues();
        }
    }

    public class ExporterUtils
    {
        private DotNetRuServerContext _context;
        private DirectoryInfo _directory;
        private XmlWriterSettings _settings;

        XmlSerializerNamespaces _emptyNamespaces;
        public ExporterUtils(DotNetRuServerContext context, DirectoryInfo directory)
        {
            this._context = context;
            this._directory = directory;
            this._settings = new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true };
            this._emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
        }

        public async Task ExportCommunties()
        {
            var entityDirectory = _directory.CreateSubdirectory(@"communities");

            var entities = await _context.Communities?
                .Select(c=>c.ToVm())
                .ToListAsync();

            var serializer = new XmlSerializer(typeof(CommunityVm));

            foreach (var entity in entities)
            {
                using (var stream = new FileStream(Path.Combine(entityDirectory.FullName.ToString(),
                    entity.Id + ".xml"), FileMode.Create))
                using (var writer = XmlWriter.Create(stream, _settings))
                {
                    serializer.Serialize(writer, entity, _emptyNamespaces);
                }
            }
        }

        public async Task ExportFriends()
        {
            var entityDirectory = _directory.CreateSubdirectory(@"friends");

            var entities = await _context.Friends
                .Select(f=>f.ToVm())
                .ToListAsync();

            var serializer = new XmlSerializer(typeof(FriendVm));

            foreach (var entity in entities)
            {
                var itemFolder = entityDirectory.CreateSubdirectory(entity.Id);
                using (var stream = new FileStream(Path.Combine(itemFolder.FullName.ToString(),
                    entity.Id + ".xml"), FileMode.Create))
                using (var writer = XmlWriter.Create(stream, _settings))
                {
                    serializer.Serialize(writer, entity, _emptyNamespaces);

                }
            }

        }
        public async Task ExportMeetups()
        {
            var entityDirectory = _directory.CreateSubdirectory(@"meetups");

            var entities = await _context
                .Meetups
                .Include(x => x.Venue)
                .Include(x => x.Friends).ThenInclude(x => x.Friend)
                .Include(x => x.Community)
                .Include(x => x.Sessions).ThenInclude(x => x.Talk)
                .Select(x=> x.ToVm()).ToListAsync();

            var serializer = new XmlSerializer(typeof(MeetupVm));


            foreach (var entity in entities)
            {
                using (var stream = new FileStream(Path.Combine(entityDirectory.FullName.ToString(),
                    entity.Id + ".xml"), FileMode.Create))
                using (var writer = XmlWriter.Create(stream, _settings))
                {
                    serializer.Serialize(writer, entity, _emptyNamespaces);

                }
            }

        }
        public async Task ExportSpeekers()
        {
            var entityDirectory = _directory.CreateSubdirectory(@"spekers");

            var entities = await _context.Speakers
                .Select(s=>s.ToVm())
                .ToListAsync();

            var serializer = new XmlSerializer(typeof(SpeakerVm));

            foreach (var entity in entities)
            {
                var itemFolder = entityDirectory.CreateSubdirectory(entity.Id);
                using (var stream = new FileStream(Path.Combine(itemFolder.FullName.ToString(),
                    entity.Id + ".xml"), FileMode.Create))
                using (var writer = XmlWriter.Create(stream, _settings))
                {
                    serializer.Serialize(writer, entity, _emptyNamespaces);

                }
            }

        }
        public async Task ExportTalks()
        {
            var entityDirectory = _directory.CreateSubdirectory(@"talks");

            var entities = await _context.Talks
                .Include(t=>t.Speakers).ThenInclude(t=>t.Speaker)
                .Include(x => x.SeeAlsoTalks)
                .Select(t=> t.ToVm())
                .ToListAsync();

            var serializer = new XmlSerializer(typeof(TalkVm));

            foreach (var entity in entities)
            {
                using (var stream = new FileStream(Path.Combine(entityDirectory.FullName.ToString(),
                    entity.Id + ".xml"), FileMode.Create))
                using (var writer = XmlWriter.Create(stream, _settings))
                {
                    serializer.Serialize(writer, entity, _emptyNamespaces);

                }
            }

        }
        public async Task ExportVenues()
        {
            var entityDirectory = _directory.CreateSubdirectory(@"venues");

            var entities = await _context.Venues.Select(v=>v.ToVm()).ToListAsync();

            var serializer = new XmlSerializer(typeof(VenueVm));

            foreach (var entity in entities)
            {
                using (var stream = new FileStream(Path.Combine(entityDirectory.FullName.ToString(),
                    entity.Id + ".xml"), FileMode.Create))
                using (var writer = XmlWriter.Create(stream, _settings))
                {
                    serializer.Serialize(writer, entity, _emptyNamespaces);

                }
            }

        }
    }
}


