namespace AgendaApi.Application.UseCases.AuthenticationUseCases.DTOs
{
    public abstract record LoginBaseResponse
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
    }
}
