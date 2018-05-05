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
    public abstract class MeetupApiController : ControllerBase
    { 
        /// <summary>
        /// Create meetup draft
        /// </summary>
        
        /// <param name="communityId"></param>
        /// <param name="meetupDraft"></param>
        /// <response code="201">Draft was sucessfully created</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Community not found</response>
        /// <response code="409">Draft is already exists</response>
        [HttpPost]
        [Route("/communities/{communityId}/meetups/draft")]
        [ValidateModelState]
        [SwaggerOperation("CreateMeetupDraft")]
        public abstract void CreateMeetupDraft([FromRoute][Required]string communityId, [FromBody]CreateMeetupDraftParameters meetupDraft);

        /// <summary>
        /// Delete meetup draft
        /// </summary>
        
        /// <param name="communityId"></param>
        /// <param name="meetupId"></param>
        /// <response code="204">Draft was successfuly deleted</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Community or meetup not found</response>
        [HttpDelete]
        [Route("/communities/{communityId}/meetups/{meetupId}/draft")]
        [ValidateModelState]
        [SwaggerOperation("DeleteMeetupDraft")]
        public abstract void DeleteMeetupDraft([FromRoute][Required]string communityId, [FromRoute][Required]string meetupId);

        /// <summary>
        /// Get meetup draft
        /// </summary>
        
        /// <param name="communityId"></param>
        /// <param name="meetupId"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Community or meetup not found</response>
        [HttpGet]
        [Route("/communities/{communityId}/meetups/{meetupId}/draft")]
        [ValidateModelState]
        [SwaggerOperation("GetMeetupDraft")]
        [SwaggerResponse(statusCode: 200, type: typeof(MeetupDraft), description: "OK")]
        public abstract MeetupDraft GetMeetupDraft([FromRoute][Required]string communityId, [FromRoute][Required]string meetupId);

        /// <summary>
        /// Update meetup draft
        /// </summary>
        
        /// <param name="communityId"></param>
        /// <param name="meetupId"></param>
        /// <param name="updateMeetupDraftProperties"></param>
        /// <response code="204">Draft was successfuly updated</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Community or meetup not found</response>
        [HttpPut]
        [Route("/communities/{communityId}/meetups/{meetupId}/draft")]
        [ValidateModelState]
        [SwaggerOperation("UpdateMeetupDraft")]
        public abstract void UpdateMeetupDraft([FromRoute][Required]string communityId, [FromRoute][Required]string meetupId, [FromBody]UpdateMeetupDraftParameters updateMeetupDraftProperties);
    }
}
