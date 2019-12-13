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

            var t = TimeZoneInfo.GetSystemTimeZones();

            var optionsBuilder = new DbContextOptionsBuilder<DotNetRuServerContext>();

            optionsBuilder.UseSqlServer(connectionString);
            var context = new DotNetRuServerContext(optionsBuilder.Options);

            var directoryInfo = new DirectoryInfo(rootDirectoryPath);
            var dbDirectory = directoryInfo.CreateSubdirectory(Path.Combine("Audit","db"));

            var export = new ExporterUtils(context, dbDirectory);

            Console.WriteLine("Starting export Communities");
            await export.ExportCommunties();

            Console.WriteLine("Starting export Friends");
            await export.ExportFriends();

            Console.WriteLine("Starting export Meetups");
            await export.ExportMeetups();

            Console.WriteLine("Starting export Speakers");
            await export.ExportSpeakers();

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

            if (_context.Communities != null)
            {
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
        }

        public async Task ExportFriends()
        {
            var entityDirectory = _directory.CreateSubdirectory(@"friends");

            var entities = await _context.Friends
                .ToListAsync();

            var serializer = new XmlSerializer(typeof(FriendVm));

            foreach (var entity in entities)
            {
                var dto = entity.ToVm();
                var itemFolder = entityDirectory.CreateSubdirectory(dto.Id);
                using (var stream = new FileStream(Path.Combine(itemFolder.FullName,
                    dto.Id + ".xml"), FileMode.Create))
                using (var writer = XmlWriter.Create(stream, _settings))
                {
                    serializer.Serialize(writer, dto, _emptyNamespaces);
                }

                var logo = await _context.Images.FirstOrDefaultAsync(x => x.Id == entity.LogoId);
                var smallLogo = await _context.Images.FirstOrDefaultAsync(x => x.Id == entity.SmallLogoId);

                await File.WriteAllBytesAsync(Path.Combine(itemFolder.ToString(), "logo.png"), logo.Data);
                await File.WriteAllBytesAsync(Path.Combine(itemFolder.ToString(), "logo.small.png"), smallLogo.Data);
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
        public async Task ExportSpeakers()
        {
            var entityDirectory = _directory.CreateSubdirectory(@"speakers");

            var entities = await _context.Speakers
                .ToListAsync();

            var serializer = new XmlSerializer(typeof(SpeakerVm));

            foreach (var entity in entities)
            {
                var dto = entity.ToVm();
                var itemFolder = entityDirectory.CreateSubdirectory(dto.Id);
                using (var stream = new FileStream(Path.Combine(itemFolder.FullName.ToString(),
                    dto.Id + ".xml"), FileMode.Create))
                using (var writer = XmlWriter.Create(stream, _settings))
                {
                    serializer.Serialize(writer, dto, _emptyNamespaces);
                }

                var logo = await _context.Images.FirstOrDefaultAsync(x => x.Id == entity.AvatarId);
                var smallLogo = await _context.Images.FirstOrDefaultAsync(x => x.Id == entity.AvatarSmallId);

                await File.WriteAllBytesAsync(Path.Combine(itemFolder.ToString(), "avatar.jpg"), logo.Data);
                await File.WriteAllBytesAsync(Path.Combine(itemFolder.ToString(), "avatar.small.jpg"), smallLogo.Data);
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


