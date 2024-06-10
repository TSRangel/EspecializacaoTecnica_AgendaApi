﻿using FluentValidation;

namespace AgendaApi.Application.UseCases.ScheduleUseCases.GetMonthSchedule
{
    public class GetMonthScheduleValidator : AbstractValidator<GetMonthScheduleRequest>
    {
        public GetMonthScheduleValidator() 
        {
            RuleFor(s => s.date).NotEmpty()
                .WithMessage("É necessário uma data como referência para filtrar os agendamentos");
        }
    }
}