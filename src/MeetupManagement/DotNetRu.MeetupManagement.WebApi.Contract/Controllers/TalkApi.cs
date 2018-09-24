#pragma warning disable SA1028 // Code must not contain trailing whitespace
/*
 * Meetup Management Service API
 *
 * Meetup Management Service API
 *
 * OpenAPI spec version: 0.1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
#pragma warning restore SA1028 // Code must not contain trailing whitespace

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DotNetRu.MeetupManagement.WebApi.Contract.Attributes;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;

namespace DotNetRu.MeetupManagement.WebApi.Contract.Controllers
#pragma warning disable SA1028 // Code must not contain trailing whitespace
{ 
    /// <inheritdoc />
    /// <summary>
    /// 
    /// </summary>
#pragma warning restore SA1028 // Code must not contain trailing whitespace
#pragma warning disable SA1649
    public abstract class TalkApiController : ControllerBase
#pragma warning restore SA1649
#pragma warning disable SA1028 // Code must not contain trailing whitespace
    { 
        /// <summary>
        /// Create talk draft
        /// </summary>
        /// <remarks>Create new talk draft.</remarks>
        /// <param name="talkDraft"></param>
        /// <response code="201">Draft was successfully created</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Draft is already exists</response>
        [HttpPost]
        [Route("/talks/draft")]
        [ValidateModelState]
        [SwaggerOperation("CreateTalkDraft")]
        public abstract void CreateTalkDraft([FromBody]CreateTalkDraftParameters talkDraft);

        /// <summary>
        /// Delete talk draft
        /// </summary>
        
        /// <param name="talkId"></param>
        /// <response code="204">Draft was successfully deleted</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Talk not found</response>
        [HttpDelete]
        [Route("/talk/{talkId}/draft")]
        [ValidateModelState]
        [SwaggerOperation("DeleteTalkDraft")]
        public abstract void DeleteTalkDraft([FromRoute][Required]string talkId);

        /// <summary>
        /// Get talk draft
        /// </summary>
        
        /// <param name="talkId"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Talk not found</response>
        [HttpGet]
        [Route("/talk/{talkId}/draft")]
        [ValidateModelState]
        [SwaggerOperation("GetTalkDraft")]
        [SwaggerResponse(statusCode: 200, type: typeof(TalkDraft), description: "OK")]
        public abstract TalkDraft GetTalkDraft([FromRoute][Required]string talkId);

        /// <summary>
        /// Update talk draft
        /// </summary>
        
        /// <param name="talkId"></param>
        /// <param name="body"></param>
        /// <response code="204">Draft was successfully updated</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Talk not found</response>
        [HttpPut]
        [Route("/talk/{talkId}/draft")]
        [ValidateModelState]
        [SwaggerOperation("UpdateTalkDraft")]
        public abstract void UpdateTalkDraft([FromRoute][Required]string talkId, [FromBody]UpdateTalkDraftParameters body);
    }
#pragma warning restore SA1028 // Code must not contain trailing whitespace
}
