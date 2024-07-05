using AgendaApi.Application.UseCases.SuperAdminUseCases.CreateSuperAdmin;
using AgendaApi.Application.UseCases.SuperAdminUseCases.DeleteSuperAdmin;
using AgendaApi.Application.UseCases.SuperAdminUseCases.GetAllSuperAdmin;
using AgendaApi.Application.UseCases.SuperAdminUseCases.GetSuperAdminByEmail;
using AgendaApi.Application.UseCases.SuperAdminUseCases.UpdateSuperAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuperAdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllSuperAdminRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetSuperAdminByEmailResponse>> GetById(string email,
            CancellationToken cancellationToken)
        {
            if (email is null) return BadRequest();

            var result = await _mediator.Send(new GetSuperAdminByEmailRequest(email), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateSuperAdminResponse>> Create(CreateSuperAdminRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{superAdminId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UpdateSuperAdminResponse>> Update(Guid? superAdminId, UpdateSuperAdminRequest request,
            CancellationToken cancellationToken)
        {
            if (superAdminId != request.superAdminId) return BadRequest();
            
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{superAdminId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DeleteSuperAdminResponse>> Delete(Guid? superAdminId, 
            CancellationToken cancellationToken)
        {
            if (superAdminId is null) return BadRequest();

            var result = await _mediator.Send(new DeleteSuperAdminRequest(superAdminId.Value), cancellationToken);
            return Ok(result);
        }
    }
}
