using System;

namespace _1_Veiculo
{
    class Program
    {
        static void Main(string[] args)
        {
            Veiculo carro = new Veiculo("Opala", "Winchester", "DEA675", "Preto", 1040.9, false, 20, 0, 150000.00);

            carro.Desligar();
            carro.Ligar();
            carro.Acelerar();
            carro.Acelerar();
            carro.Frear();
            carro.Frear();
            carro.Frear();
            carro.Abastecer(20);
            carro.Pintar("Amarelo");
            carro.Ligar();
            carro.Desligar();
        }
    }
}
