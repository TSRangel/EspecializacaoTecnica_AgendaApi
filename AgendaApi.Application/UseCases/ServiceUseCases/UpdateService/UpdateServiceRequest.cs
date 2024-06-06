﻿using MediatR;

namespace AgendaApi.Application.UseCases.ServiceUseCase.UpdateService
{
    public sealed record UpdateServiceRequest(
        Guid id,
        string? name,
        string? description,
        TimeSpan duration,
        decimal price,
        Guid legalEntityId)
        : IRequest<UpdateServiceResponse>;
}
