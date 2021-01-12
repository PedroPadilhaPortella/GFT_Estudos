namespace entity.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, Categoria: {this.Nome}";
        }
    }
}