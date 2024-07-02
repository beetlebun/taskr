using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Dtos;
using taskr.Models;

namespace taskr.Mappers
{
    public static class TaskMapper
    {
        public static Models.Task ToModel(this TaskRequest taskRequest)
        {
            return new Models.Task
            {
                Name = taskRequest.Name,
                Description = taskRequest.Description,
                ProjectId = taskRequest.ProjectId
            };
        }

        public static TaskResponse ToResponse(this Models.Task task)
        {
            return new TaskResponse
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                ProjectId = task.ProjectId
            };
        }
    }
}