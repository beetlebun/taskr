using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Dtos;
using taskr.Models;

namespace taskr.Mappers
{
    public static class CollaboratorMappers
    {
        public static Collaborator ToModel(this CollaboratorRequest collaboratorRequest)
        {
            return new Collaborator
            {
                Name = collaboratorRequest.Name,
                UserId = collaboratorRequest.UserId
            };
        }

        public static CollaboratorResponse ToResponse(this Collaborator collaborator)
        {
            return new CollaboratorResponse
            {
                Id = collaborator.Id,
                Name = collaborator.Name,
                UserId = collaborator.UserId
            };
        }
    }
}