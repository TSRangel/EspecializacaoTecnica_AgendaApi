﻿using FluentValidation;

namespace AgendaApi.Application.UseCases.LegalPersonUseCases.DeleteLegalEntity
{
    public class DeleteLegalEntityValidator : AbstractValidator<DeleteLegalEntityRequest>
    {
        public DeleteLegalEntityValidator() 
        {
            RuleFor(le => le.Id).NotEmpty();
        }
    }
}
