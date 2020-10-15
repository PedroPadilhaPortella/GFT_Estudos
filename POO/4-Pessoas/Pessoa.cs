using System;

namespace _4_Pessoas
{
    class Pessoa
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Pessoa(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return $"{Name} -> {Age}";
        }

        
    }
}
