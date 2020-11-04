using System.ComponentModel.DataAnnotations;

namespace FuncionariosWA.Models
{
    public class GFT
    {
       public int Id { get; set; }
       [Required]
       public string Nome { get; set; }
       [Required]
       public string Cep { get; set; }
       [Required]
       public string Endereco  { get; set; }
       [Required]
       public string Cidade { get; set; }
       [Required]
       public string Estado { get; set; }
       public bool Status { get; set; }
    }
}