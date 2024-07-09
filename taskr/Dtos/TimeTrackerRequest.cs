using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Dtos
{
    public class TimeTrackerRequest
    {
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public string TimeZoneId { get; set; } = string.Empty;
        [Required]
        public Guid TaskId { get; set; }
        [Required]
        public Guid CollaboratorId { get; set; }
    }
}