using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Dtos;
using taskr.Models;

namespace taskr.Repository
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid id);
        Task<List<Project>> GetAllAsync();
        Task<Project> CreateAsync(Project project);
        Task<Project?> UpdateAsync(Guid id, ProjectRequest projectRequest);
        Task<Project?> DeleteAsync(Guid id);
    }
}