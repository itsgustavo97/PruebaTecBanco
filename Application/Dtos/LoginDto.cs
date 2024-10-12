namespace Application.Dtos
{
    public class LoginDto
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }

        public LoginDto(string id, string nombres, string apellidos, DateTime fechaNacimiento, string direccion, string phoneNumber, string email, string password, string accessToken)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
            Direccion = direccion;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            AccessToken = accessToken;
        }
    }
}
