namespace AgendaApi.Domain.Entities
{
    public class NaturalPerson : Person
    {
        public Guid NaturalPersonId { get; set; }
        public ICollection<Scheduling>? Schedulings { get; set; }
    }
}
