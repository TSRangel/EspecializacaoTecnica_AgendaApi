namespace AgendaApi.Domain.Entities
{
    public class ServiceStatus : BaseEntity
    {
        public int ServiceStatusId { get; set; }
        public string? StatusName { get; set; }
        public ICollection<Service>? Services { get; set; }
    }
}
