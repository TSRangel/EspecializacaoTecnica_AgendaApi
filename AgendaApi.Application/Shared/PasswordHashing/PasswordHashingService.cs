namespace AgendaApi.Application.Shared.PasswordHashing
{
    public static class PasswordHashingService
    {
        public static string Hash(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public static bool Verify(string providedPassword, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }   
}
