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

namespace taskr.Controllers
{
    [ApiController]
    [Route("api/timetracker")]
    public class TimeTrackerController : ControllerBase
    {
        private readonly ITimeTrackerRepository timeTrackerRepository;
        private readonly ITaskRepository taskRepository;
        private readonly ICollaboratorRepository collaboratorRepository;

        public TimeTrackerController(ITimeTrackerRepository timeTrackerRepository, ITaskRepository taskRepository, ICollaboratorRepository collaboratorRepository)
        {
            this.timeTrackerRepository = timeTrackerRepository;
            this.taskRepository = taskRepository;
            this.collaboratorRepository = collaboratorRepository;
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

            var timeTracker = await this.timeTrackerRepository.GetByIdAsync(id);

            if (timeTracker == null)
            {
                errors.Add("Id", new List<string> { $"Apontamento com Id {{{id}}} não encontrado" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return Ok(timeTracker!.ToResponse());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var timeTrackers = await this.timeTrackerRepository.GetAllAsync();
            var timeTrackerResponses = timeTrackers.Select(timeTracker => timeTracker.ToResponse());

            return Ok(timeTrackerResponses);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TimeTrackerRequest timeTrackerRequest)
        {
            var errors = new Dictionary<string, List<string>> {};

            var task = await this.taskRepository.GetByIdAsync(timeTrackerRequest.TaskId);
            var collaborator = await this.collaboratorRepository.GetByIdAsync(timeTrackerRequest.CollaboratorId);

            if (task == null)
            {
                errors.Add("TaskId", new List<string> { $"Tarefa com Id {{{timeTrackerRequest.TaskId}}} não encontrada" });
            }

            if (collaborator == null)
            {
                errors.Add("CollaboratorId", new List<string> { $"Colaborador com Id {{{timeTrackerRequest.CollaboratorId}}} não encontrado" });
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeTracker = timeTrackerRequest.ToModel();

            var existingTimeTrackers = await this.timeTrackerRepository.GetAllAsync();

            if (!timeTracker.ValidEndDate())
            {
                errors.Add("EndDate", new List<string> { $"Data de início deve ser menor ou igual à data de término" });
            }

            if (!timeTracker.ValidTrackingAmountInDay(existingTimeTrackers))
            {
                errors.Add("TrackingAmount", new List<string> { $"Total de horas em tarefas no dia não deve ultrapassar 24h" });
            }

            if (!timeTracker.ValidTrackingTime(existingTimeTrackers))
            {
                errors.Add("TrackingIntegrity", new List<string> { $"Não é possível a inclusão de um apontamento em uma tarefa caso já possua um apontamento no mesmo horário em outra" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            await this.timeTrackerRepository.CreateAsync(timeTracker);

            return CreatedAtAction(nameof(GetById), new { id = timeTracker.Id }, timeTracker.ToResponse());
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TimeTrackerRequest timeTrackerRequest)
        {
            var errors = new Dictionary<string, List<string>> {};

            var task = await this.taskRepository.GetByIdAsync(timeTrackerRequest.TaskId);
            var collaborator = await this.collaboratorRepository.GetByIdAsync(timeTrackerRequest.CollaboratorId);

            if (task == null)
            {
                errors.Add("TaskId", new List<string> { $"Tarefa com Id {{{timeTrackerRequest.TaskId}}} não encontrada" });
            }

            if (collaborator == null)
            {
                errors.Add("CollaboratorId", new List<string> { $"Colaborador com Id {{{timeTrackerRequest.CollaboratorId}}} não encontrado" });
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeTracker = await this.timeTrackerRepository.UpdateAsync(id, timeTrackerRequest);

            if (timeTracker == null)
            {
                errors.Add("Id", new List<string> { $"Apontamento com Id {{{id}}} não encontrado" });
            }

            var existingTimeTrackers = await this.timeTrackerRepository.GetAllAsync();

            if (!timeTracker!.ValidEndDate())
            {
                errors.Add("EndDate", new List<string> { $"Data de início deve ser menor ou igual à data de término" });
            }

            if (!timeTracker!.ValidTrackingAmountInDay(existingTimeTrackers))
            {
                errors.Add("TrackingAmount", new List<string> { $"Total de horas em tarefas no dia não deve ultrapassar 24h" });
            }

            if (!timeTracker!.ValidTrackingTime(existingTimeTrackers))
            {
                errors.Add("TrackingIntegrity", new List<string> { $"Não é possível a inclusão de um apontamento em uma tarefa caso já possua um apontamento no mesmo horário em outra" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return Ok(timeTracker.ToResponse());
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

            var timeTracker = await this.timeTrackerRepository.DeleteAsync(id);

            if (timeTracker == null)
            {
                errors.Add("Id", new List<string> { $"Apontamento com Id {{{id}}} não encontrado" });
            }

            if (!errors.IsNullOrEmpty()) return new BadRequestObjectResult( new { errors } );

            return NoContent();
        }
    }
}