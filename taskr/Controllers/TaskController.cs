using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using taskr.Dtos;
using taskr.Interfaces;
using taskr.Mappers;
using taskr.Repository;

namespace taskr.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;
        private readonly IProjectRepository projectRepository;

        public TaskController(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            this.taskRepository = taskRepository;
            this.projectRepository = projectRepository;
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

            var task = await this.taskRepository.GetByIdAsync(id);

            if (task == null)
            {
                errors.Add("Id", new List<string> { $"Tarefa com Id {{{id}}} não encontrada" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return Ok(task!.ToResponse());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await this.taskRepository.GetAllAsync();
            var taskResponses = tasks.Select(task => task.ToResponse());

            return Ok(taskResponses);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskRequest taskRequest)
        {
            var errors = new Dictionary<string, List<string>> {};

            var project = await this.projectRepository.GetByIdAsync(taskRequest.ProjectId);

            if (project == null)
            {
                errors.Add("ProjectId", new List<string> { $"Projeto com Id {{{taskRequest.ProjectId}}} não encontrado" });
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            var task = taskRequest.ToModel();

            await this.taskRepository.CreateAsync(task);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task.ToResponse());
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TaskRequest taskRequest)
        {
            var errors = new Dictionary<string, List<string>> {};

            var project = await this.projectRepository.GetByIdAsync(taskRequest.ProjectId);

            if (project == null)
            {
                errors.Add("ProjectId", new List<string> { $"Projeto com Id {{{taskRequest.ProjectId}}} não encontrado" });
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await this.taskRepository.UpdateAsync(id, taskRequest);

            if (task == null)
            {
                errors.Add("Id", new List<string> { $"Tarefa com Id {{{id}}} não encontrada" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return Ok(task!.ToResponse());
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

            var task = await this.taskRepository.DeleteAsync(id);

            if (task == null)
            {
                errors.Add("Id", new List<string> { $"Tarefa com Id {{{id}}} não encontrada" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return NoContent();
        }
    }
}