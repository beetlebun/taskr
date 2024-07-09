using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Dtos;
using taskr.Models;

namespace taskr.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<Collaborator?> GetByIdAsync(Guid id);
        Task<List<Collaborator>> GetAllAsync();
        Task<Collaborator> CreateAsync(Collaborator collaborator);
        Task<Collaborator?> UpdateAsync(Guid id, CollaboratorRequest collaboratorRequest);
        Task<Collaborator?> DeleteAsync(Guid id);
    }
}