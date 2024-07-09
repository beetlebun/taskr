using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Dtos;
using taskr.Models;

namespace taskr.Interfaces
{
    public interface ITimeTrackerRepository
    {
        Task<TimeTracker?> GetByIdAsync(Guid id);
        Task<List<TimeTracker>> GetAllAsync();
        Task<TimeTracker> CreateAsync(TimeTracker timeTracker);
        Task<TimeTracker?> UpdateAsync(Guid id, TimeTrackerRequest timeTrackerRequest);
        Task<TimeTracker?> DeleteAsync(Guid id);
    }
}