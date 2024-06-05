﻿using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingStatusUseCase.DeleteSchedulingStatus
{
    public sealed record DeleteSchedulingStatusRequest(Guid id)
        : IRequest<DeleteSchedulingStatusResponse>;
}
