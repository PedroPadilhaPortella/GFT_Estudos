namespace FuncionariosWAClean.Domain.Entities
{
    public class VagaTecnologia : Entity
    {
        public int TecnologiaId { get; set; }
        public int VagaId { get; set; }
        public Tecnologia Tecnologia { get; set; }
        public Vaga Vaga { get; set; }

        public VagaTecnologia() { }
    }
}