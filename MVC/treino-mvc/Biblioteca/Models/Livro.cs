using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Livro
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage="Precisa de um Título!")]
        public string Titulo { get; set; }
        [Required(ErrorMessage="Precisa de um Autor!")]
        public string Autor { get; set; }
        [Required(ErrorMessage="Precisa de uma Quantidade de páginas!")]
        public int QuantidadeDePaginas { get; set; }
        [Required(ErrorMessage="Precisa de uma Quantidade de exemplares!")]
        public int QuantidadeDeExemplares { get; set; }

        public Livro() { }
        public Livro(int id, string titulo, string autor, int quantidadeDePaginas, int quantidadeDeExemplares){
            Id = id;
            Titulo = titulo;
            Autor = autor;
            QuantidadeDePaginas = quantidadeDePaginas;
            QuantidadeDeExemplares = quantidadeDeExemplares;
        }
    }
}