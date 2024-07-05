using AgendaApi.Domain.Interfaces;

namespace AgendaApi.Application.Shared.GlobalValidators
{
    public static class EmailValidator
    {
        public static async Task<bool> IsValidEmail(this IUnitOfWork unitOfWork, string email, CancellationToken cancellationToken)
        {
            var legalEntityValidation = await unitOfWork.LegalEntityRepository!.GetByEmail(le => le.Email == email, cancellationToken);
            var naturalPersonValidation = await unitOfWork.NaturalPersonRepository!.GetByEmail(np => np.Email == email, cancellationToken);
            return legalEntityValidation != null || naturalPersonValidation != null;
        }
    }
}
