using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.WeekDayUseCases.GetAllWeekDay
{
    public sealed class GetAllWeekDayHandler
        : IRequestHandler<GetAllWeekDayRequest, List<GetAllWeekDayResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllWeekDayHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllWeekDayResponse>> Handle(GetAllWeekDayRequest request,
            CancellationToken cancellationToken)
        {
            var weekDays = await _unitOfWork.WeekDayRepository!.GetAllById(request.legalEntityId , cancellationToken);
            return _mapper.Map<List<GetAllWeekDayResponse>>(weekDays);
        }
    }
}
