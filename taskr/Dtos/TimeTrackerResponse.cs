using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Dtos
{
    public class TimeTrackerResponse
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeZoneId { get; set; } = string.Empty;
        public Guid TaskId { get; set; }
        public Guid CollaboratorId { get; set; }
    }
}