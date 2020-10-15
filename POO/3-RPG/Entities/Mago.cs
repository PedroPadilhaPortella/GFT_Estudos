using System;
using System.Collections.Generic;

namespace Personagens
{
    class Mago: Personagem
    {
        private List<string> Magias { get; set; }
        public Mago(string nome, int vida, int mana, double xp, int inteligencia, int forca, int level, List<string> magias)
            :base(nome, vida, mana, xp, inteligencia, forca, level)
        {
            Magias = magias;
        }
        public override void LvlUp()
        {
            Mana += 10;
            Inteligencia += 10;
            Level++;
            System.Console.WriteLine($"{Nome} agora é nivel {Level}");
        }
        public void AprenderMagia(string magia){
            Magias.Add(magia);
            System.Console.WriteLine($"Magia {magia} aprendida");
        }
        public override void Attack()
        {
            Random rand = new Random();
            int NumeroAtaque = rand.Next(0, 300);
            double ataque = (Inteligencia * Level) + (int)NumeroAtaque;
            System.Console.WriteLine($"O ataque do {Nome} foi de {ataque}");
        }
    }
}