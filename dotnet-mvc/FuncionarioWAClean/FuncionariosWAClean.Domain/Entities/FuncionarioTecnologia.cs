namespace FuncionariosWAClean.Domain.Entities
{
    public class FuncionarioTecnologia : Entity
    {
        public int FuncionarioId { get; set; }
        public int TecnologiaId { get; set; }
        public Funcionario Funcionario { get; set; }
        public Tecnologia Tecnologia { get; set; }

        public FuncionarioTecnologia() { }
    }
}