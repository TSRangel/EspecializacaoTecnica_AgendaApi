using AgendaApi.Application.Shared.GlobalValidators;
using FluentValidation;
using System.Globalization;

namespace AgendaApi.Application.UseCases.TimetableUseCases.UpdateTimetable
{
    public class UpdateTimetableValidator : AbstractValidator<UpdateTimetableRequest>
    {
        public UpdateTimetableValidator() 
        {
            RuleFor(tt => tt.id).NotEmpty().NotNull()
                .Must(GuidValidator.BeValid);
            RuleFor(tt => tt.startTime).NotEmpty().NotNull()
                .Must(TimeOnlyValidFormat)
                .WithMessage("O tempo deve estar no formato HH:mm.");
            RuleFor(tt => tt.endTime).NotEmpty().NotNull()
                .Must(TimeOnlyValidFormat)
                .WithMessage("O tempo deve estar no formato HH:mm.");
            RuleFor(tt => tt.legalEntityId).NotEmpty().NotNull()
                .Must(GuidValidator.BeValid);
            RuleFor(tt => tt.weekDayId).NotEmpty().NotNull()
                .InclusiveBetween(1, 7)
                .WithMessage("O identificador do dia da semana deve estar entre 1 e 7.");
        }
        private bool TimeOnlyValidFormat(TimeOnly time) =>
            TimeOnly.TryParseExact(time.ToString(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
