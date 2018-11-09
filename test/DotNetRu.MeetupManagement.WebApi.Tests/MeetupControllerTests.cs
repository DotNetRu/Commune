namespace DotNetRu.MeetupManagement.WebApi.Tests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Principal;
    using DotNetRu.MeetupManagement.Application.Contract.Services;
    using DotNetRu.MeetupManagement.Domain.Contract.Exceptions;
    using DotNetRu.MeetupManagement.WebApi.Contract.Models;
    using DotNetRu.MeetupManagement.WebApi.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class MeetupControllerTests
    {
        [Fact]
        public void CreateMeetupSuccessTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.CreateMeetupDraft("111-111", "Good name"));
            initializedObjects.Controller.CreateMeetupDraft("111-111", new CreateMeetupDraftParameters { Id = "1", Name = "Good name" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        public void CreateMeetupInvalidParametersTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.Controller.CreateMeetupDraft(string.Empty, new CreateMeetupDraftParameters { Id = "1", Name = "Good name" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateMeetupUnauthorizedTest()
        {
            var initializedObjects = this.Init(false);
            initializedObjects.ServiceDependency.Setup(s => s.CreateMeetupDraft("111-111", "Good name"));
            initializedObjects.Controller.CreateMeetupDraft("111-111", new CreateMeetupDraftParameters { Id = "1", Name = "Good name" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void CreateMeetupCommunityNotFoundTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.CreateMeetupDraft(It.IsAny<string>(), "Good name")).Throws(new CommunityNotFoundException(string.Empty, string.Empty));
            initializedObjects.Controller.CreateMeetupDraft("111-111", new CreateMeetupDraftParameters { Id = "1", Name = "Good name" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public void CreateMeetupDraftAlreadyExistsTest()
        {
            // should not pass. We don't have such an exception in domain level contract
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.CreateMeetupDraft("111-111", "Good name"));
            initializedObjects.Controller.CreateMeetupDraft("111-111", new CreateMeetupDraftParameters { Id = "1", Name = "Good name" });

            // Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        public void DeleteMeetupDraftSuccessTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.DeleteMeetupDraft("111-111", "1"));
            initializedObjects.Controller.DeleteMeetupDraft("111-111", "1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [Fact]
        public void DeleteMeetupDraftUnauthorizedTest()
        {
            var initializedObjects = this.Init(false);
            initializedObjects.ServiceDependency.Setup(s => s.DeleteMeetupDraft("111-111", "1"));
            initializedObjects.Controller.DeleteMeetupDraft("111-111", "1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void DeleteMeetupDraftNotFoundTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.DeleteMeetupDraft("111-111", "1")).Throws(new CommunityNotFoundException(string.Empty, string.Empty));

            initializedObjects.Controller.DeleteMeetupDraft("111-111", "1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetMeetupDraftSuccessTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.GetMeetupDraft("111-111", "1")).Returns(() => new Application.Contract.Models.MeetupDraft("1", "111-111") { Friends = new List<Application.Contract.Models.FriendReference>(), Venue = new Application.Contract.Models.VenueReference("1", "Venue", false) });

            initializedObjects.Controller.GetMeetupDraft("111-111", "1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public void GetMeetupDraftUnauthorizedTest()
        {
            var initializedObjects = this.Init(false);
            initializedObjects.ServiceDependency.Setup(s => s.GetMeetupDraft("111-111", "1"));

            initializedObjects.Controller.GetMeetupDraft("111-111", "1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void GetMeetupDraftEntityNotFoundTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.GetMeetupDraft("111-111", "1")).Throws(new CommunityNotFoundException(string.Empty, string.Empty));

            initializedObjects.Controller.GetMeetupDraft("111-111", "1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NotFound);
            initializedObjects.ServiceDependency.Setup(s => s.GetMeetupDraft("111-111", "1")).Throws(new MeetupNotFoundException(string.Empty, string.Empty));

            initializedObjects.Controller.GetMeetupDraft("111-111", "1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public void UpdateMeetupDraftSuccessTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.UpdateMeetupDraft("111-111", "1", null, null, "1"));

            initializedObjects.Controller.UpdateMeetupDraft("111-111", "1", new UpdateMeetupDraftParameters());
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [Fact]
        public void UpdateMeetupDraftInvalidParametersTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.Controller.UpdateMeetupDraft("111-111", string.Empty, new UpdateMeetupDraftParameters());
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void UpdateMeetupDraftUnauthorizedTest()
        {
            var initializedObjects = this.Init(false);
            initializedObjects.ServiceDependency.Setup(s => s.UpdateMeetupDraft("111-111", "1", null, null, "1"));

            initializedObjects.Controller.UpdateMeetupDraft("111-111", "1", new UpdateMeetupDraftParameters());
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void UpdateMeetupDraftEntityNotFoundTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.UpdateMeetupDraft("111-111", "1", null, null, "1")).Throws(new CommunityNotFoundException(string.Empty, string.Empty));
            initializedObjects.Controller.UpdateMeetupDraft("111-111", "1", new UpdateMeetupDraftParameters() { VenueId = "1" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NotFound);

            initializedObjects.ServiceDependency.Setup(s => s.UpdateMeetupDraft("111-111", "1", null, null, "1")).Throws(new MeetupNotFoundException(string.Empty, string.Empty));
            initializedObjects.Controller.UpdateMeetupDraft("111-111", "1", new UpdateMeetupDraftParameters() { VenueId = "1" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NotFound);
        }

        private MockedObjects Init(bool isAuthenticated)
        {
            var loggerDependency = Mock.Of<ILogger<MeetupController>>();
            var serviceDependency = new Mock<IMeetupDraftService>();
            var meetupController = new MeetupController(loggerDependency, serviceDependency.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = new GenericPrincipal(new GenericIdentity(isAuthenticated ? "1" : string.Empty), null) }
                }
            };
            return new MockedObjects { ServiceDependency = serviceDependency, Controller = meetupController };
        }

        internal class MockedObjects
        {
            internal Mock<IMeetupDraftService> ServiceDependency;
            internal MeetupController Controller;
        }
    }
}
