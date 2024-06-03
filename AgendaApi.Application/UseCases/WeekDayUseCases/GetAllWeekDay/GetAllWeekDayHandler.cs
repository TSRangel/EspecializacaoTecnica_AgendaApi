﻿using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.WeekDayUseCases.GetAllWeekDay
{
    public sealed class GetAllWeekDayHandler
        : IRequestHandler<GetAllWeekDayRequest, List<FreeSchedulingResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllWeekDayHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<FreeSchedulingResponse>> Handle(GetAllWeekDayRequest request,
            CancellationToken cancellationToken)
        {
            var weekDays = await _unitOfWork.WeekDayRepository.GetAll(cancellationToken);
            //return _mapper.Map<List<GetAllWeekDayResponse>>(weekDays);
            List<AvailableTimeDTO> availableTimeList = new List<AvailableTimeDTO>();
            foreach (var weekDay in weekDays)
            {
                foreach (var timetable in weekDay.Timetables)
                {
                    while (timetable.StartTime < timetable.EndTime)
                    {
                        availableTimeList.Add(new AvailableTimeDTO
                        {
                            StartTime = timetable.StartTime,
                            WeekDayId = timetable.WeekDayId
                        });
                        timetable.StartTime = timetable.StartTime.AddMinutes(30);
                    }
                }
            }
            return _mapper.Map<List<FreeSchedulingResponse>>(availableTimeList);
        }
    }
}