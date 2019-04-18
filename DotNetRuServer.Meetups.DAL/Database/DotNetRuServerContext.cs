using DevActivator.Meetups.DAL.Database;
using DotNetRuSerber.Meetups.BL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServer.Meetups.DAL.Database
{
    public class DotNetRuServerContext : DbContext
    {
        public DotNetRuServerContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Speaker>().BindSpeaker();
            modelBuilder.Entity<Talk>().BindTalk();

            modelBuilder.Entity<SeeAlsoTalk>().BindSeeAlsoTalk();
            modelBuilder.Entity<SpeakerTalk>().BindSpeakerTalk();

            modelBuilder.Entity<Venue>().BindVenue();
            modelBuilder.Entity<Community>().BindCommunities();
            modelBuilder.Entity<Friend>().BindFriend();
            modelBuilder.Entity<Meetup>().BindMeetup();
            modelBuilder.Entity<FriendAtMeetup>().BindFriendAtMeetup();
            modelBuilder.Entity<Session>().BindSession();
        }

        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }

        // M2M-tables
        public DbSet<SeeAlsoTalk> SeeAlsoTalks { get; set; }
        public DbSet<SpeakerTalk> SpeakerTalks { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Meetup> Meetups { get; set; }
        public DbSet<FriendAtMeetup> FriendAtMeetups { get; set; }
        
        public DbSet<Session> Sessions { get; set; }
    }
}