/*
 * Meetup Management Service API
 *
 * Meetup Management Service API
 *
 * OpenAPI spec version: 0.1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using DotNetRu.MeetupManagement.WebApi.Contract.Attributes;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;

namespace DotNetRu.MeetupManagement.WebApi.Contract.Controllers
{ 
    /// <inheritdoc />
    /// <summary>
    /// 
    /// </summary>
    public abstract class TalkApiController : ControllerBase
    { 
        /// <summary>
        /// Create talk draft
        /// </summary>
        /// <remarks>Create new talk draft.</remarks>
        /// <param name="talkDraft"></param>
        /// <response code="201">Draft was sucessfully created</response>
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
        /// <response code="204">Draft was successfuly deleted</response>
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
        /// <remarks>Get talk draft</remarks>
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
        /// <response code="204">Draft was successfuly updated</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Talk not found</response>
        [HttpPut]
        [Route("/talk/{talkId}/draft")]
        [ValidateModelState]
        [SwaggerOperation("UpdateTalkDraft")]
        public abstract void UpdateTalkDraft([FromRoute][Required]string talkId, [FromBody]UpdateTalkDraftParameters body);
    }
}
