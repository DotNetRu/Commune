namespace DotNetRu.MeetupManagement.WebApi.Tests
{
    using System.Net;
    using System.Security.Principal;
    using DotNetRu.MeetupManagement.Application.Contract.Models;
    using DotNetRu.MeetupManagement.Application.Contract.Services;
    using DotNetRu.MeetupManagement.WebApi.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class FriendControllerTests
    {
        [Fact]
        public void CreateFriendSuccessTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.CreateFriendDraft(new CreateFriendDraftParameters() { Name = "New friend" }));
            initializedObjects.Controller.CreateFriendDraft(new Contract.Models.CreateFriendDraftParameters() { Name = "New friend" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        public void CreateFriendUnauthorizedTest()
        {
            var initializedObjects = this.Init(false);
            initializedObjects.Controller.CreateFriendDraft(new Contract.Models.CreateFriendDraftParameters() { Name = "New friend" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void CreateFriendInvalidParamsTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.Controller.CreateFriendDraft(new Contract.Models.CreateFriendDraftParameters());
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void UpdateFriendSuccessTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.UpdateFriendDraft(new FriendDraft("1") { Name = "New friend", Description = "Description" }));
            initializedObjects.Controller.UpdateFriendDraft("1", new Contract.Models.UpdateFriendDraftParameters() { Name = "New friend", Description = "Description" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [Fact]
        public void UpdateFriendUnauthorizedTest()
        {
            var initializedObjects = this.Init(false);
            initializedObjects.Controller.UpdateFriendDraft("1", new Contract.Models.UpdateFriendDraftParameters() { Name = "New friend", Description = "Description" });
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void UpdateFriendInvalidParamsTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.Controller.UpdateFriendDraft("1", new Contract.Models.UpdateFriendDraftParameters());
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeleteFriendSuccessTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.ServiceDependency.Setup(s => s.DeleteFriendDraft("1"));
            initializedObjects.Controller.DeleteFriendDraft("1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [Fact]
        public void DeleteFriendUnauthorizedTest()
        {
            var initializedObjects = this.Init(false);
            initializedObjects.Controller.DeleteFriendDraft("1");
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void DeleteFriendInvalidParamsTest()
        {
            var initializedObjects = this.Init(true);
            initializedObjects.Controller.DeleteFriendDraft(string.Empty);
            Assert.Equal(initializedObjects.Controller.Response.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        private MockedObjects Init(bool isAuthenticated)
        {
            var loggerDependency = Mock.Of<ILogger<FriendController>>();
            var serviceDependency = new Mock<IFriendDraftService>();
            var controller = new FriendController(loggerDependency, serviceDependency.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = new GenericPrincipal(new GenericIdentity(isAuthenticated ? "1" : string.Empty), null) }
                }
            };
            return new MockedObjects { ServiceDependency = serviceDependency, Controller = controller };
        }

        internal class MockedObjects {
            internal Mock<IFriendDraftService> ServiceDependency;
            internal FriendController Controller;
        }
    }
}
