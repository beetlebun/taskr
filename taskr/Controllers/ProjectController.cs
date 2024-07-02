using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskr.Dtos;
using taskr.Mappers;
using taskr.Repository;

namespace taskr.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await this.projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                // transfomar em objeto padrão de erro resposta
                var errors = new Dictionary<string, List<string>>
                {
                    { "Id", new List<string> { $"Projeto com id {{{id}}} não encontrado" } }
                };

                return new NotFoundObjectResult( new { errors } );
            }

            return Ok(project.ToResponse());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await this.projectRepository.GetAllAsync();
            var projectResponses = projects.Select(project => project.ToResponse());

            return Ok(projectResponses);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectRequest projectRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = projectRequest.ToModel();

            await this.projectRepository.CreateAsync(project);

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project.ToResponse());
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectRequest projectRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await this.projectRepository.UpdateAsync(id, projectRequest);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project.ToResponse());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await this.projectRepository.DeleteAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}