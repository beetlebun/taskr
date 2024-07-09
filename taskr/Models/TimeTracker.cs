using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Models
{
    [Table("TimeTrackers")]
    public class TimeTracker
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeZoneId { get; set; } = string.Empty;
        public Guid TaskId { get; set; }
        public Task? Task { get; set; }
        public Guid CollaboratorId { get; set; }
        public Collaborator? Collaborator { get; set; }

        public bool ValidEndDate()
        {
            return EndDate >= StartDate;
        }

        public bool ValidTrackingAmountInDay(List<TimeTracker> existingTimeTrackers)
        {
            var totalHours = existingTimeTrackers
                .Where(tt => tt.CollaboratorId == CollaboratorId && tt.StartDate.Date == StartDate.Date)
                .Sum(tt => (tt.EndDate - tt.StartDate).TotalHours);

            totalHours += (EndDate - StartDate).TotalHours;

            return totalHours <= 24;
        }

        public bool ValidTrackingTime(List<TimeTracker> existingTimeTrackers)
        {
            foreach (var tracker in existingTimeTrackers.Where(tt => tt.CollaboratorId == CollaboratorId))
            {
                if ((StartDate < tracker.EndDate && EndDate > tracker.StartDate) ||
                    (EndDate > tracker.StartDate && StartDate < tracker.EndDate))
                {
                    return false;
                }
            }

            return true;
        }
    }
}