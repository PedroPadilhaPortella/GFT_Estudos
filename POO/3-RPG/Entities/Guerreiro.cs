using System;
using System.Collections.Generic;

namespace Personagens
{
    class Guerreiro: Personagem
    {
        private List<string> Habilidades { get; set; }
        public Guerreiro(string nome, int vida, int mana, double xp, int inteligencia, int forca, int level, List<string> habilidades)
            :base(nome, vida, mana, xp, inteligencia, forca, level)
        {
            Habilidades = habilidades;
        }
        public override void LvlUp()
        {
            Vida += 10;
            Forca += 10;
            Level++;
            System.Console.WriteLine($"{Nome} agora é nivel {Level}");
        }
        public void AprenderHabilidade(string skill){
            Habilidades.Add(skill);
            System.Console.WriteLine($"Habilidade {skill} aprendida");
        }
        public override void Attack()
        {
            Random rand = new Random();
            int NumeroAtaque = rand.Next(0, 300);
            double ataque = (Forca * Level) + NumeroAtaque;
            System.Console.WriteLine($"O ataque do {Nome} foi de {ataque}");
        }
    }
}