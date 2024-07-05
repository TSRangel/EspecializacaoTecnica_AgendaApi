using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.TimetableUseCases.DeleteTimetable
{
    public sealed class DeleteTimetableHandler
        : IRequestHandler<DeleteTimetableRequest, List<DeleteTimetableResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteTimetableHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<DeleteTimetableResponse>> Handle(DeleteTimetableRequest request, 
            CancellationToken cancellationToken)
        {
            var timetables = await _unitOfWork.TimetableRepository!.Delete(cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<List<DeleteTimetableResponse>>(timetables);
        }
    }
}
