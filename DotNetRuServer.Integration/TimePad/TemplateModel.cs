using System.Collections.Generic;

namespace DotNetRuServer.Integration.TimePad
{
    public class TemplateModel
    {
        public List<TemplateAgendaItem> Agenda { get; set; }
        public List<TemplateTalk> Talks { get; set; }
        
        public class TemplateAgendaItem
        {
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Speakers { get; set; }
            public string TalkTitle { get; set; }
        }
        
        public class TemplateTalk
        {
            public string Speakers { get; set; }
            public string TalkTitle { get; set; }
            public string TalkDescription { get; set; }
            public List<string> SpeakersDescriptions { get; set; }
        }
    }
}