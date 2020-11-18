namespace FuncionariosWA.Models
{
    public class FuncionarioTecnologia
    {
        public int FuncionarioId { get; set; }
        public int TecnologiaId { get; set; }
        public Funcionario Funcionario { get; set; }
        public Tecnologia Tecnologia { get; set; }

        public FuncionarioTecnologia() { }
        public FuncionarioTecnologia(Funcionario funcionario, Tecnologia tecnologia)
        {
            Funcionario = funcionario;
            Tecnologia = tecnologia;
        }
    }
}