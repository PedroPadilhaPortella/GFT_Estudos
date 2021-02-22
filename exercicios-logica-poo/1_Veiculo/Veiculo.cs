namespace _1_Veiculo{
    class Veiculo{
        private string Marca { get; set; }
        private string Modelo { get; set; }
        private string Placa { get; set; }
        private string Cor { get; set; }
        private double Km { get; set; }
        private bool IsLigado { get; set; }
        private int LitrosCombustivel { get; set; }
        private int Velocidade { get; set; }
        private double Preco { get; set; }

        public Veiculo(string marca, string modelo, string placa, string cor, double km, bool isLigado, int litrosCombustivel, int velocidade, double preco ){
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Cor = cor;
            Km = km;
            IsLigado = isLigado;
            LitrosCombustivel = litrosCombustivel;
            Velocidade = velocidade;
            Preco = preco;
        }

        public void Acelerar()
        {
            Velocidade += 20;
            LitrosCombustivel -= 10;
            System.Console.WriteLine($"Velocidade = {Velocidade}");
        }

        public void Frear(){
            if(Velocidade <= 0){
                System.Console.WriteLine("O veiculo está parado.");
            }else{
                Velocidade -= 20;
                System.Console.WriteLine($"Velocidade = {Velocidade}");
            }
        }
        public void Abastecer(int combustivel)
        {
            LitrosCombustivel += combustivel;
            if(LitrosCombustivel > 60){
                LitrosCombustivel = 60;
            }else if(LitrosCombustivel < 0){
                LitrosCombustivel = 0;
            }
        }
        public void Pintar(string cor){
            Cor = cor;
            System.Console.WriteLine($"Agora o carro é {Cor}");
        }
        public void Ligar(){
            if(IsLigado){
                System.Console.WriteLine("O veiculo já esta ligado");
            }else{
                IsLigado = true;
            }
        }
        public void Desligar(){
            if(!IsLigado){
                System.Console.WriteLine("O veiculo já esta desligado");
            }else{
                IsLigado = false;
            }
        }
    }
}