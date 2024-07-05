using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.TimetableUseCases.UpdateTimetable
{
    public sealed class UpdateTimetableHandler
        : IRequestHandler<UpdateTimetableRequest, UpdateTimetableResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateTimetableHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateTimetableResponse> Handle(UpdateTimetableRequest request,
            CancellationToken cancellationToken)
        {
            var timetable = await _unitOfWork.TimetableRepository!.GetById(
                tt => tt.TimetableId == request.id, cancellationToken);
            if (timetable is null) throw new NotFoundException("Intervalo de tempo não encontrado.");

            _mapper.Map(request, timetable);
            _unitOfWork.TimetableRepository.Update(timetable);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateTimetableResponse>(timetable);
        }
    }
}
