using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.WeekDayUseCases.CreateWeekDay
{
    public sealed class CreateWeekDayHandler :
        IRequestHandler<CreateWeekDayRequest, CreateWeekDayResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateWeekDayHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateWeekDayResponse> Handle(CreateWeekDayRequest request,
            CancellationToken cancellationToken)
        {
            var weekDay = _mapper.Map<WeekDay>(request);
            _unitOfWork.WeekDayRepository!.Create(weekDay);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateWeekDayResponse>(weekDay);
        }
    }
}
