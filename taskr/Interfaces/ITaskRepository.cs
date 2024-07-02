using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Dtos;

namespace taskr.Interfaces
{
    public interface ITaskRepository
    {
        Task<Models.Task?> GetByIdAsync(Guid id);
        Task<List<Models.Task>> GetAllAsync();
        Task<Models.Task> CreateAsync(Models.Task task);
        Task<Models.Task?> UpdateAsync(Guid id, TaskRequest taskRequest);
        Task<Models.Task?> DeleteAsync(Guid id);
    }
}