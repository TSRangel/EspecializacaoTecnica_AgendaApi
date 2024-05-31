﻿using AgendaApi.Domain.Entities;

namespace AgendaApi.Domain.Interfaces
{
    public interface INaturalPersonRepository : IBaseRepository<NaturalPerson>
    {
        Task<NaturalPerson> GetByEmail(string email, CancellationToken cancellationToken);
    }
}
