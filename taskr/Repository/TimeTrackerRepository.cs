using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using taskr.Data;
using taskr.Dtos;
using taskr.Interfaces;
using taskr.Models;

namespace taskr.Repository
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        private readonly ApplicationDBContext context;

        public TimeTrackerRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<TimeTracker?> GetByIdAsync(Guid id)
        {
            return await this.context.TimeTrackers.FirstOrDefaultAsync(timeTracker => timeTracker.Id == id);
        }

        public async Task<List<TimeTracker>> GetAllAsync()
        {
            return await this.context.TimeTrackers.ToListAsync();
        }

        public async Task<TimeTracker> CreateAsync(TimeTracker timeTracker)
        {
            await this.context.TimeTrackers.AddAsync(timeTracker);
            await this.context.SaveChangesAsync();

            return timeTracker;
        }

        public async Task<TimeTracker?> UpdateAsync(Guid id, TimeTrackerRequest timeTrackerRequest)
        {
            var timeTracker = await this.context.TimeTrackers.FirstOrDefaultAsync(timeTracker => timeTracker.Id == id);

            if (timeTracker == null)
            {
                return null;
            }

            {
                timeTracker.StartDate = timeTrackerRequest.StartDate;
                timeTracker.EndDate = timeTrackerRequest.EndDate;
                timeTracker.TimeZoneId = timeTrackerRequest.TimeZoneId;
                timeTracker.TaskId = timeTrackerRequest.TaskId;
                timeTracker.CollaboratorId = timeTrackerRequest.CollaboratorId;
            }

            await this.context.SaveChangesAsync();

            return timeTracker;
        }

        public async Task<TimeTracker?> DeleteAsync(Guid id)
        {
            var timeTracker = await this.context.TimeTrackers.FirstOrDefaultAsync(timeTracker => timeTracker.Id == id);

            if (timeTracker == null)
            {
                return null;
            }

            this.context.TimeTrackers.Remove(timeTracker);
            await this.context.SaveChangesAsync();

            return timeTracker;
        }
    }
}