using Bunit;
using DotNetRu.Commune.WasmClient.Model;
using DotNetRu.Commune.WasmClient.Pages;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace DotNetRu.Commune.Test.WasmClient
{
    public class IndexPageTests
    {
        [Fact]
        public void PageContents_WhenCustomMessageNotEmpty_AddsInformationFromCustomMesage()
        {
            //arrange
            var myCustomMsg = "my custom msg";
            var ctx = new TestContext();
            ctx.Services.Configure<AuditSettings>(message => message.RepositoryName = myCustomMsg);
            ctx.Services.AddLogging();

            //act
            var sut = ctx.RenderComponent<Index>();

            //assert
            var contentDiv = sut.FindAll("#message");
            contentDiv.Should().HaveCount(1).And
                .SatisfyRespectively(x => x.MarkupMatches($"<div id=\"message\">customMsgOpts.Value.Message == \"{myCustomMsg}\""));
            sut.Find("#notNullInfo").MarkupMatches("<div id=\"notNullInfo\">customMsgOpts.Value.Message is not null or empty</div>");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void PageContents_WhenCustomMessageIsNullOrEmpty_AddsEmptyinfoBlockDoesNotAddsInfo(string emptyMsg)
        {
            //arrange
            var ctx = new TestContext();
            ctx.Services.Configure<AuditSettings>(message => message.RepositoryName = emptyMsg);
            ctx.Services.AddLogging();

            //act
            var sut = ctx.RenderComponent<Index>();

            //assert
            var contentDiv = sut.FindAll("#message");
            contentDiv.Should().BeEmpty();
            sut.Find("#nullInfo").MarkupMatches("<div id=\"nullInfo\">customMsgOpts.Value.Message is null or empty</div>");
        }
    }
}
