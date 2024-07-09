using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Dtos;
using taskr.Models;

namespace taskr.Mappers
{
    public static class TimeTrackerMappers
    {
        public static TimeTracker ToModel(this TimeTrackerRequest timeTrackerRequest)
        {
            return new TimeTracker
            {
                StartDate = timeTrackerRequest.StartDate,
                EndDate = timeTrackerRequest.EndDate,
                TimeZoneId  = timeTrackerRequest.TimeZoneId,
                TaskId = timeTrackerRequest.TaskId,
                CollaboratorId = timeTrackerRequest.CollaboratorId
            };
        }

        public static TimeTrackerResponse ToResponse(this TimeTracker timeTracker)
        {
            return new TimeTrackerResponse
            {
                Id = timeTracker.Id,
                StartDate = timeTracker.StartDate,
                EndDate = timeTracker.EndDate,
                TimeZoneId  = timeTracker.TimeZoneId,
                TaskId = timeTracker.TaskId,
                CollaboratorId = timeTracker.CollaboratorId
            };
        }
    }
}