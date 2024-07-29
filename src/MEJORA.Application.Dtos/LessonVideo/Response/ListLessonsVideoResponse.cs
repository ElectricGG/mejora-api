using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEJORA.Application.Dtos.LessonVideo.Response
{
    public class ListLessonsVideoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HtmlContent { get; set; }
        public string FormattedDuration { get; set; }
        public int PlayOrder { get; set; }
        public bool HasBeenPlayed { get; set; }
        public int SecondsElapsed { get; set; }
        public bool IsLastVideoSeen { get; set; }
    }
}
