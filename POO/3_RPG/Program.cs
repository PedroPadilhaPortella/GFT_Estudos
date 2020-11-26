using System;
using System.Collections.Generic;
using Personagens;

namespace _3_RPG
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> magias = new List<string>();
            magias.Add("Expeliarmus");
            magias.Add("Accio");
            Mago mago1 = new Mago("Dumbbledor", 100, 200, 0, 150, 50, 1, magias);

            List<string> skills = new List<string>();
            skills.Add("Machadada Vertical de Odin");
            skills.Add("Atordoamento");
            Guerreiro guerreiro1 = new Guerreiro("Ragnar", 400, 20, 0, 50, 500, 1, skills);

            mago1.AprenderMagia("Avadaquedava");
            guerreiro1.AprenderHabilidade("Derrocada dos 1000 demonios");
            mago1.LvlUp();
            guerreiro1.LvlUp();
            mago1.Attack();
            guerreiro1.Attack();

            System.Console.WriteLine("Quantidade de Personagens: " + Personagem.TotalDePersonagens);
        }
    }
}
