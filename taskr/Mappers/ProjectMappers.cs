using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Dtos;
using taskr.Models;

namespace taskr.Mappers
{
    public static class ProjectMappers
    {
        public static Project ToModel(this ProjectRequest projectRequest)
        {
            return new Project
            {
                Name = projectRequest.Name,
            };
        }

        public static ProjectResponse ToResponse(this Project project)
        {
            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
            };
        }
    }
}