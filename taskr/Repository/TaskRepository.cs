using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using taskr.Data;
using taskr.Dtos;
using taskr.Interfaces;

namespace taskr.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDBContext context;

        public TaskRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<Models.Task?> GetByIdAsync(Guid id)
        {
            return await this.context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        }

        public async Task<List<Models.Task>> GetAllAsync()
        {
            return await this.context.Tasks.ToListAsync();
        }

        public async Task<Models.Task> CreateAsync(Models.Task task)
        {
            await this.context.Tasks.AddAsync(task);
            await this.context.SaveChangesAsync();

            return task;
        }

        public async Task<Models.Task?> UpdateAsync(Guid id, TaskRequest taskRequest)
        {
            var task = await this.context.Tasks.FirstOrDefaultAsync(task => task.Id == id);

            if (task == null)
            {
                return null;
            }

            {
                task.Name = taskRequest.Name;
                task.Description = taskRequest.Description;
                task.ProjectId = taskRequest.ProjectId;
            }

            await this.context.SaveChangesAsync();

            return task;
        }

        public async Task<Models.Task?> DeleteAsync(Guid id)
        {
            var task = await this.context.Tasks.FirstOrDefaultAsync(task => task.Id == id);

            if (task == null)
            {
                return null;
            }

            this.context.Tasks.Remove(task);
            await this.context.SaveChangesAsync();

            return task;
        }
    }
}