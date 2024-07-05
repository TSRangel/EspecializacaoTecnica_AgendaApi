using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.WeekDayUseCases.DeleteWeekDay
{
    public sealed class DeleteWeekDayHandler 
        : IRequestHandler<DeleteWeekDayRequest, DeleteWeekDayResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteWeekDayHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DeleteWeekDayResponse> Handle(DeleteWeekDayRequest request, 
            CancellationToken cancellationToken)
        {
            var weekDay = await _unitOfWork.WeekDayRepository!.GetById(
                wd => wd.WeekDayId == request.id, cancellationToken);

            if (weekDay is null) throw new NotFoundException("Dia da semana não encontrado.");

            _unitOfWork.WeekDayRepository.Delete(weekDay);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<DeleteWeekDayResponse>(weekDay);
        }
    }
}
