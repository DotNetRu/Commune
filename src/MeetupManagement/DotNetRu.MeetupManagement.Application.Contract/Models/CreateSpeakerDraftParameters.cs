using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class CreateSpeakerDraftParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string BlogsUrl { get; set; }
        public string ContactsUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string GitHubUrl { get; set; }
        public Company Company { get; set; }
    }
}
