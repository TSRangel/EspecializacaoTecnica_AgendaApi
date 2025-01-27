﻿using AgendaApi.Application.UseCases.SchedulingUseCases.ConfirmeScheduling;
using AgendaApi.Application.UseCases.SchedulingUseCases.CreateScheduling;
using AgendaApi.Application.UseCases.SchedulingUseCases.DeleteScheduling;
using AgendaApi.Application.UseCases.SchedulingUseCases.EndsNotPayedScheduling;
using AgendaApi.Application.UseCases.SchedulingUseCases.GetAllNaturalPersonSchedulingsBySchedulingStatus;
using AgendaApi.Application.UseCases.SchedulingUseCases.GetAllScheduling;
using AgendaApi.Application.UseCases.SchedulingUseCases.GetAllSchedulingByLegalEntity;
using AgendaApi.Application.UseCases.SchedulingUseCases.GetAllSchedulingByNaturalPerson;
using AgendaApi.Application.UseCases.SchedulingUseCases.GetSchedulingById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SchedulingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SchedulingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,PJ,PF")]
        public async Task<ActionResult<List<GetAllSchedulingResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllSchedulingRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{schedulingId:Guid}")]
        [Authorize(Roles = "Admin,PJ,PF")]
        public async Task<ActionResult<GetSchedulingByIdResponse>> GetById(Guid? schedulingId, CancellationToken cancellationToken)
        {
            if (schedulingId is null) return BadRequest();
            var result = await _mediator.Send(new GetSchedulingByIdRequest(schedulingId.Value), cancellationToken);
            return Ok(result);
        }

        [HttpGet("legalEntity/{legalEntityId:Guid}")]
        [Authorize(Roles = "Admin,PJ")]
        public async Task<ActionResult<List<GetAllSchedulingByLegalEntityResponse>>>
            GetAllByLegalEntity(Guid? legalEntityId, CancellationToken cancellationToken)
        {
            if (legalEntityId is null) return BadRequest();
            var result = await _mediator.Send(new GetAllSchedulingByLegalEntityRequest(legalEntityId.Value), cancellationToken);
            return Ok(result);
        }

        [HttpGet("naturalPerson/{naturalPersonId:Guid}")]
        [Authorize(Roles = "Admin,PF")]
        public async Task<ActionResult<List<GetAllSchedulingByNaturalPersonResponse>>>
            GetAllByNaturalPerson(Guid? naturalPersonId, CancellationToken cancellationToken)
        {
            if (naturalPersonId is null) return BadRequest();
            var result = await _mediator.Send(new GetAllSchedulingByNaturalPersonRequest(naturalPersonId.Value), cancellationToken);
            return Ok(result);
        }

        [HttpGet("naturalPersonBySchedulingStatus/{naturalPersonId:Guid}")]
        [Authorize(Roles = "Admin,PJ,PF")]
        public async Task<ActionResult<List<GetAllNaturalPersonSchedulingsBySchedulingStatusResponse>>>
            GetAllNaturalPersonSchedulingBySchedulingStatusId(Guid? naturalPersonId, int? schedulingStatusId, 
            CancellationToken cancellationToken)
        {
            if (naturalPersonId is null || schedulingStatusId is null) return BadRequest();
            var result = await _mediator.Send(
                new GetAllNaturalPersonSchedulingsBySchedulingStatusRequest(naturalPersonId.Value, schedulingStatusId.Value), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PJ,PF")]
        public async Task<ActionResult<CreateSchedulingResponse>> Create(CreateSchedulingRequest request,
            CancellationToken cancellationToken)
        {
            if (request is null) return BadRequest();
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("endsPayedScheduling/{schedulingId:Guid}")]
        [Authorize(Roles = "Admin,PJ")]
        public async Task<ActionResult<EndsPayedSchedulingResponse>> Payed(Guid? schedulingId,
            CancellationToken cancellationToken)
        {
            if (schedulingId is null) return BadRequest();
            var result = await _mediator.Send(new EndsPayedSchedulingRequest(schedulingId.Value), cancellationToken);
            return Ok(result);
        }

        [HttpPut("endsNotPayedScheduling/{schedulingId:Guid}")]
        [Authorize(Roles = "Admin,PJ")]
        public async Task<ActionResult<EndsNotPayedSchedulingResponse>> NotPayed(Guid? schedulingId,
            CancellationToken cancellationToken)
        {
            if (schedulingId is null) return BadRequest();
            var result = await _mediator.Send(new EndsNotPayedSchedulingRequest(schedulingId.Value), cancellationToken);
            return Ok(result);
        }

        [HttpPut("cancel/{schedulingId:Guid}")]
        [Authorize(Roles = "Admin,PJ,PF")]
        public async Task<ActionResult<CancelSchedulingResponse>> Cancel(Guid? schedulingId, CancelSchedulingRequest request,
            CancellationToken cancellationToken)
        {
            if (request.schedulingId != schedulingId) return BadRequest();
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
