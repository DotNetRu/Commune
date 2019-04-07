using DevActivator.Meetups.BL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevActivator.Meetups.DAL.Database
{
    public static class DatabaseMappings
    {
        public static void BindSpeaker(this EntityTypeBuilder<Speaker> speaker)
        {
            speaker.ToTable("Speakers");

            speaker.Property(x => x.Id).HasColumnName("Id");
            speaker.Property(x => x.Name).HasColumnName("Name");
            speaker.Property(x => x.CompanyName).HasColumnName("CompanyName");
            speaker.Property(x => x.CompanyUrl).HasColumnName("CompanyUrl");
            speaker.Property(x => x.Description).HasColumnName("Description");
            speaker.Property(x => x.BlogUrl).HasColumnName("BlogUrl");
            speaker.Property(x => x.ContactsUrl).HasColumnName("ContactsUrl");
            speaker.Property(x => x.TwitterUrl).HasColumnName("TwitterUrl");
            speaker.Property(x => x.HabrUrl).HasColumnName("HabrUrl");
            speaker.Property(x => x.GitHubUrl).HasColumnName("GitHubUrl");
        }

        public static void BindTalk(this EntityTypeBuilder<Talk> talk)
        {
            talk.ToTable("Talks");

            talk.Property(x => x.Id).HasColumnName("Id");
            talk.Property(x => x.Title).HasColumnName("Title");
            talk.Property(x => x.Description).HasColumnName("Description");
            talk.Property(x => x.CodeUrl).HasColumnName("CodeUrl");
            talk.Property(x => x.SlidesUrl).HasColumnName("SlidesUrl");
            talk.Property(x => x.VideoUrl).HasColumnName("VideoUrl");
        }

        public static void BindSpeakerTalk(this EntityTypeBuilder<SpeakerTalk> speakerTalk)
        {
            speakerTalk.ToTable("SpeakerTalks");
            speakerTalk.HasKey(t => new {t.SpeakerId, t.TalkId});

            speakerTalk.Property(x => x.SpeakerId).HasColumnName("SpeakerId");
            speakerTalk.Property(x => x.TalkId).HasColumnName("TalkId");

            speakerTalk.HasOne(x => x.Talk).WithMany(x => x.Speakers).HasForeignKey(x => x.TalkId);
            speakerTalk.HasOne(x => x.Speaker).WithMany(x => x.Talks).HasForeignKey(x => x.SpeakerId);
        }

        public static void BindSeeAlsoTalk(this EntityTypeBuilder<SeeAlsoTalk> seeAlsoTalk)
        {
            seeAlsoTalk.ToTable("SeeAlsoTalks");

            seeAlsoTalk.HasKey(t => new {t.ParentTalkId, t.ChildTalkId});

            seeAlsoTalk.Property(x => x.ParentTalkId).HasColumnName("ParentTalkId");
            seeAlsoTalk.Property(x => x.ChildTalkId).HasColumnName("ChildTalkId");

            seeAlsoTalk.HasOne(x => x.ParentTalk).WithMany(x => x.SeeAlsoTalks).HasForeignKey(x => x.ParentTalkId);
            seeAlsoTalk.HasOne(x => x.ChildTalk);
        }

        public static void BindVenue(this EntityTypeBuilder<Venue> venue)
        {
            venue.ToTable("Venues");

            venue.Property(x => x.Id).HasColumnName("Id");
            venue.Property(x => x.ExportId).HasColumnName("ExportId");
            venue.Property(x => x.Name).HasColumnName("Name");
            venue.Property(x => x.City).HasColumnName("City");
            venue.Property(x => x.MapUrl).HasColumnName("MapUrl");
            venue.Property(x => x.Address).HasColumnName("Address");
        }

        public static void BindCommunities(this EntityTypeBuilder<Community> community)
        {
            community.ToTable("Communities");

            community.Property(x => x.Id).HasColumnName("Id");
            community.Property(x => x.ExportId).HasColumnName("ExportId");
            community.Property(x => x.Name).HasColumnName("Name");
            community.Property(x => x.City).HasColumnName("City");
            community.Property(x => x.TimeZone).HasColumnName("TimeZone");
        }

        public static void BindFriend(this EntityTypeBuilder<Friend> friend)
        {
            friend.ToTable("Friends");

            friend.Property(x => x.Id).HasColumnName("Id");
            friend.Property(x => x.ExportId).HasColumnName("ExportId");
            friend.Property(x => x.Name).HasColumnName("Name");
            friend.Property(x => x.Description).HasColumnName("Description");
            friend.Property(x => x.Url).HasColumnName("Url");
            friend.Property(x => x.LogoUrl).HasColumnName("LogoUrl");
            friend.Property(x => x.SmallLogoUrl).HasColumnName("SmallLogoUrl");
        }

        public static void BindMeetup(this EntityTypeBuilder<Meetup> meetup)
        {
            meetup.ToTable("Meetups");

            meetup.Property(x => x.Id).HasColumnName("Id");
            meetup.Property(x => x.ExportId).HasColumnName("ExportId");
            meetup.Property(x => x.Name).HasColumnName("Name");

            meetup.Property(x => x.VenueId).HasColumnName("VenueId");
            meetup.Property(x => x.CommunityId).HasColumnName("CommunityId");

            meetup.HasOne(x => x.Venue).WithMany(x => x.Meetups);
            meetup.HasOne(x => x.Community).WithMany(x => x.Meetups);
            meetup.HasMany(x => x.Sessions).WithOne(x => x.Meetup);
            meetup.HasMany(x => x.Friends).WithOne(x => x.Meetup);
        }

        public static void BindSession(this EntityTypeBuilder<Session> session)
        {
            session.ToTable("Sessions");

            session.Property(x => x.Id).HasColumnName("Id");
            session.Property(x => x.TalkId).HasColumnName("TalkId");
            session.Property(x => x.MeetupId).HasColumnName("MeetupId");

            session.Property(x => x.StartTime).HasColumnName("StartTime");
            session.Property(x => x.EndTime).HasColumnName("EndTime");

            session.HasOne(x => x.Meetup).WithMany(x => x.Sessions);
        }

        public static void BindFriendAtMeetup(this EntityTypeBuilder<FriendAtMeetup> friendAtMeetup)
        {
            friendAtMeetup.ToTable("FriendAtMeetup");

            friendAtMeetup.HasKey(t => new {t.MeetupId, t.FriendId});

            friendAtMeetup.Property(x => x.MeetupId).HasColumnName("MeetupId");
            friendAtMeetup.Property(x => x.FriendId).HasColumnName("FriendId");

            friendAtMeetup.HasOne(x => x.Friend);
            friendAtMeetup.HasOne(x => x.Meetup).WithMany(x => x.Friends);
        }
    }
}