using AgendaApi.Application.Shared.GlobalValidators;
using FluentValidation;
using System.Globalization;

namespace AgendaApi.Application.UseCases.TimetableUseCases.CreateTimetable
{
    public class CreateMultipleTimetableValidator : AbstractValidator<CreateMultipleTimetableRequest>
    {
        public CreateMultipleTimetableValidator() 
        {
            RuleForEach(tt => tt.TimetableRequests).SetValidator(new CreateTimetableValidator());
        }
    }
    public class CreateTimetableValidator : AbstractValidator<CreateTimetableRequest>
    {
        public CreateTimetableValidator() 
        {
            RuleFor(tt => tt.startTime).NotEmpty()
                .Must(TimeOnlyValidFormat)
                .WithMessage("O tempo deve estar no formato HH:mm.");
            RuleFor(tt => tt.endTime).NotEmpty()
                .Must(TimeOnlyValidator.Format)
                .WithMessage("O tempo deve estar no formato HH:mm.");
            RuleFor(tt => tt.legalEntityId).NotEmpty()
                .Must(GuidValidator.BeValid);
            RuleFor(tt => tt.weekDayId).NotEmpty()
                .InclusiveBetween(1, 7)
                .WithMessage("O identificador do dia da semana deve estar entre 1 e 7.");
        }

        private bool TimeOnlyValidFormat(TimeOnly time) => 
            TimeOnly.TryParseExact(time.ToString(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
