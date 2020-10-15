using System;

namespace _4_Pessoas
{
    class Pessoa
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public static int totalPessoas = 0;
        public Pessoa(string name, int age)
        {
            Name = name;
            Age = age;
            totalPessoas++;
        }

        public override string ToString()
        {
            return $"{Name} -> {Age}";
        }
    }
}
