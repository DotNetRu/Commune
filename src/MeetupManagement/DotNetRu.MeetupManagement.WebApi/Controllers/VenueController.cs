using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DotNetRu.MeetupManagement.Application.Contract.Services;
using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class VenueController : VenueApiController
    {
        private readonly ILogger<VenueController> _logger;
        private readonly IVenueDraftService _venueDraftService;

        public VenueController(ILogger<VenueController> logger, IVenueDraftService venueDraftService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _venueDraftService = venueDraftService ?? throw new ArgumentNullException(nameof(venueDraftService));
        }

        public override void CreateVenueDraft([FromBody] CreateVenueDraftParameters venueDraft)
        {
            throw new NotImplementedException();
        }

        public override void DeleteVenueDraft([FromRoute, Required] string venueId)
        {
            throw new NotImplementedException();
        }

        public override VenueDraft GetVenueDrafts([FromRoute, Required] string venueId)
        {
            throw new NotImplementedException();
        }

        public override void UpdateVenueDraft([FromRoute, Required] string venueId, [FromBody] UpdateVenueDraftParameters properties)
        {
            throw new NotImplementedException();
        }
    }
}
