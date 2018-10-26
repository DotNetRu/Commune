namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Configurations;
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Extensions;
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models;
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Common;
    using DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts;
    using Microsoft.EntityFrameworkCore;

    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<FriendDraft> FriendDrafts { get; set; }

        public virtual DbSet<MeetupDraft> MeetupDrafts { get; set; }

        public virtual DbSet<SpeakerDraft> SpeakerDrafts { get; set; }

        public virtual DbSet<TalkDraft> TalkDrafts { get; set; }

        public virtual DbSet<TalkRehearsal> TalkRehearsals { get; set; }

        public virtual DbSet<VenueDraft> VenueDrafts { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<EntityReference> EntityReferences { get; set; }

        public virtual DbSet<Friend> Friends { get; set; }

        public virtual DbSet<Speaker> Speakers { get; set; }

        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddConfiguration<CompanyConfiguration, Company>();
            modelBuilder.AddConfiguration<EntityReferenceConfiguration, EntityReference>();
            modelBuilder.AddConfiguration<FriendConfiguration, Friend>();
            modelBuilder.AddConfiguration<FriendDraftConfiguration, FriendDraft>();
            modelBuilder.AddConfiguration<MeetupDraftConfiguration, MeetupDraft>();
            modelBuilder.AddConfiguration<PersonConfiguration, Person>();
            modelBuilder.AddConfiguration<SpeakerConfiguration, Speaker>();
            modelBuilder.AddConfiguration<SpeakerDraftConfiguration, SpeakerDraft>();
            modelBuilder.AddConfiguration<TalkDraftConfiguration, TalkDraft>();
            modelBuilder.AddConfiguration<TalkRehearsalConfiguration, TalkRehearsal>();
            modelBuilder.AddConfiguration<VenueConfiguration, Venue>();
            modelBuilder.AddConfiguration<VenueDraftConfiguration, VenueDraft>();
        }
    }
}
