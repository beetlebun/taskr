using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using taskr.Dtos;
using taskr.Interfaces;
using taskr.Mappers;
using taskr.Models;

namespace taskr.Controllers
{
    [ApiController]
    [Route("api/collaborator")]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorRepository collaboratorRepository;
        private readonly UserManager<User> userManager;

        public CollaboratorController(ICollaboratorRepository collaboratorRepository, UserManager<User> userManager)
        {
            this.collaboratorRepository = collaboratorRepository;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var errors = new Dictionary<string, List<string>> {};

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collaborator = await this.collaboratorRepository.GetByIdAsync(id);

            if (collaborator == null)
            {
                errors.Add("Id", new List<string> { $"Colaborador com Id {{{id}}} não encontrado" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return Ok(collaborator!.ToResponse());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var collaborators = await this.collaboratorRepository.GetAllAsync();
            var collaboratorResponses = collaborators.Select(collaborator => collaborator.ToResponse());

            return Ok(collaboratorResponses);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CollaboratorRequest collaboratorRequest)
        {
            var errors = new Dictionary<string, List<string>> {};

            var user = await userManager.FindByIdAsync(collaboratorRequest.UserId);

            if (user == null)
            {
                errors.Add("UserId", new List<string> { $"Usuário com Id {{{collaboratorRequest.UserId}}} não encontrado" });
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            var collaborator = collaboratorRequest.ToModel();

            await this.collaboratorRepository.CreateAsync(collaborator);

            return CreatedAtAction(nameof(GetById), new { id = collaborator.Id }, collaborator.ToResponse());
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CollaboratorRequest collaboratorRequest)
        {
            var errors = new Dictionary<string, List<string>> {};

            var user = await userManager.FindByIdAsync(collaboratorRequest.UserId);

            if (user == null)
            {
                errors.Add("UserId", new List<string> { $"Usuário com Id {{{collaboratorRequest.UserId}}} não encontrado" });
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collaborator = await this.collaboratorRepository.UpdateAsync(id, collaboratorRequest);

            if (collaborator == null)
            {
                errors.Add("Id", new List<string> { $"Colaborador com Id {{{id}}} não encontrado" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return Ok(collaborator!.ToResponse());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var errors = new Dictionary<string, List<string>> {};

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collaborator = await this.collaboratorRepository.DeleteAsync(id);

            if (collaborator == null)
            {
                errors.Add("Id", new List<string> { $"Colaborador com Id {{{id}}} não encontrado" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return NoContent();
        }
    }
}