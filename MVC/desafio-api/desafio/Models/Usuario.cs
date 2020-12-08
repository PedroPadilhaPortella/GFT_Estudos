namespace desafio.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Usuario() { }
        public Usuario(int id, string email, string senha)
        {
            this.Id = id;
            this.Email = email;
            this.Senha = senha;
        }
    }
}