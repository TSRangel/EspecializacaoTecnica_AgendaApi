﻿using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.NaturalPersonUseCases.GetNaturalPersonByEmail
{
    public sealed class GetNaturalPersonByEmailHandler
        : IRequestHandler<GetNaturalPersonByEmailRequest, GetNaturalPersonByEmailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNaturalPersonByEmailHandler(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetNaturalPersonByEmailResponse> Handle(GetNaturalPersonByEmailRequest request,
            CancellationToken cancellationToken)
        {
            var naturalPerson = await _unitOfWork.NaturalPersonRepository.GetByEmail(request.email, cancellationToken);
            if (naturalPerson is null) return default;

            return _mapper.Map<GetNaturalPersonByEmailResponse>(naturalPerson);
        }
    }
}
