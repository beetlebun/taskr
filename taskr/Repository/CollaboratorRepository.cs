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
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly ApplicationDBContext context;

        public CollaboratorRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<Collaborator?> GetByIdAsync(Guid id)
        {
            return await this.context.Collaborators.FirstOrDefaultAsync(collaborator => collaborator.Id == id);
        }

        public async Task<List<Collaborator>> GetAllAsync()
        {
            return await this.context.Collaborators.ToListAsync();
        }

        public async Task<Collaborator> CreateAsync(Collaborator collaborator)
        {
            await this.context.Collaborators.AddAsync(collaborator);
            await this.context.SaveChangesAsync();

            return collaborator;
        }

        public async Task<Collaborator?> UpdateAsync(Guid id, CollaboratorRequest collaboratorRequest)
        {
            var collaborator = await this.context.Collaborators.FirstOrDefaultAsync(collaborator => collaborator.Id == id);

            if (collaborator == null)
            {
                return null;
            }

            {
                collaborator.Name = collaboratorRequest.Name;
                collaborator.UserId = collaboratorRequest.UserId;
            }

            await this.context.SaveChangesAsync();

            return collaborator;
        }

        public async Task<Collaborator?> DeleteAsync(Guid id)
        {
            var collaborator = await this.context.Collaborators.FirstOrDefaultAsync(collaborator => collaborator.Id == id);

            if (collaborator == null)
            {
                return null;
            }

            this.context.Collaborators.Remove(collaborator);
            await this.context.SaveChangesAsync();

            return collaborator;
        }
    }
}