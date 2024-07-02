using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using taskr.Data;
using taskr.Dtos;
using taskr.Models;

namespace taskr.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDBContext context;

        public ProjectRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await this.context.Projects.FirstOrDefaultAsync(project => project.Id == id);
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await this.context.Projects.ToListAsync();
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await this.context.Projects.AddAsync(project);
            await this.context.SaveChangesAsync();

            return project;
        }

        public async Task<Project?> UpdateAsync(Guid id, ProjectRequest projectRequest)
        {
            var project = await this.context.Projects.FirstOrDefaultAsync(project => project.Id == id);

            if (project == null)
            {
                return null;
            }

            {
                project.Name = projectRequest.Name;
            }

            await this.context.SaveChangesAsync();

            return project;
        }

        public async Task<Project?> DeleteAsync(Guid id)
        {
            var project = await this.context.Projects.FirstOrDefaultAsync(project => project.Id == id);

            if (project == null)
            {
                return null;
            }

            this.context.Projects.Remove(project);
            await this.context.SaveChangesAsync();

            return project;
        }
    }
}