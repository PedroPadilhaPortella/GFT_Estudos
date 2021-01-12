namespace FuncionariosWA.Models
{
    public class VagaTecnologia
    {
        public int TecnologiaId { get; set; }
        public int VagaId { get; set; }
        public Tecnologia Tecnologia { get; set; }
        public Vaga Vaga { get; set; }


        public VagaTecnologia() { }
        public VagaTecnologia(Vaga vaga, Tecnologia tecnologia)
        {
            Vaga = vaga;
            Tecnologia = tecnologia;
        }
    }
}