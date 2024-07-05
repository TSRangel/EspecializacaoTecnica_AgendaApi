using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.WeekDayUseCases.GetWeekDayById
{
    public sealed class GetWeekDayByIdHandler
        : IRequestHandler<GetWeekDayByIdRequest, GetWeekDayByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetWeekDayByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetWeekDayByIdResponse> Handle(GetWeekDayByIdRequest request,
            CancellationToken cancellationToken)
        {
            var weekDay = await _unitOfWork.WeekDayRepository!.GetById(wd => wd.WeekDayId == request.id, cancellationToken);
            return _mapper.Map<GetWeekDayByIdResponse>(weekDay);
        }
    }
}
