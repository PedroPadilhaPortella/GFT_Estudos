using Newtonsoft.Json;

namespace NotaFiscal.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}