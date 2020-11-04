namespace FuncionariosWA.Models
{
    public class VagaTecnologia
    {
        public int Id { get; set; }
        public int VagaId { get; set; }
        public int TecnologiaId { get; set; }
        public Vaga Vaga  { get; set; }
        public Tecnologia Tecnologia  { get; set; }
    }
}