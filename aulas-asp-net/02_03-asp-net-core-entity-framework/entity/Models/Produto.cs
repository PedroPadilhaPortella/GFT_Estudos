namespace entity.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public virtual Categoria Categoria { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, Nome: {this.Nome}, Categoria[{this.Categoria.ToString()}]";
        }
    }
}