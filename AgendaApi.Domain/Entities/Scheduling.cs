namespace AgendaApi.Domain.Entities
{
    public class Scheduling : BaseEntity
    {
        public Guid SchedulingId { get; set; }
        public DateTime SchedulingDate { get; set; }
        public int SchedulingStatusId { get; set; }
        public SchedulingStatus? SchedulingStatus { get; set; }
        public Guid NaturalPersonId { get; set; }
        public NaturalPerson? NaturalPerson { get; set; }
        public Guid LegalEntityId { get; set; }
        public LegalEntity? LegalEntity { get; set; }
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }
        public Cancellation? Cancellation { get; set; }
    }
}
