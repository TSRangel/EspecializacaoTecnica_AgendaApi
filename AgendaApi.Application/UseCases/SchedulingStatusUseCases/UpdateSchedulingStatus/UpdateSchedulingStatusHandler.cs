using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingStatusUseCase.UpdateSchedulingStatus
{
    public sealed class UpdateSchedulingStatusHandler
        : IRequestHandler<UpdateSchedulingStatusRequest, UpdateSchedulingStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateSchedulingStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateSchedulingStatusResponse> Handle(UpdateSchedulingStatusRequest request,
            CancellationToken cancellationToken) 
        {
            try
            {
            var schedulingStatus = await _unitOfWork.SchedulingStatusRepository!.GetById(ss => ss.SchedulingStatusId == request.schedulingStatusId, cancellationToken);
            if (schedulingStatus is null) throw new NotFoundException("Status de agendamento não encontrado.");

            _mapper.Map(request, schedulingStatus);
            _unitOfWork.SchedulingStatusRepository.Update(schedulingStatus);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateSchedulingStatusResponse>(schedulingStatus);

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
