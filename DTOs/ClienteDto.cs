namespace API_de_Clientes__sin_autenticación_.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaDeRegistro { get; set; }
    }
}
