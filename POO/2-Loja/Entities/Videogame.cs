using System;
using System.Collections.Generic;
using System.Globalization;

namespace _2_Loja.Entities
{
    class Videogame : Produto, Imposto
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public bool IsUsado { get; set; }
        public Videogame(){}
        public Videogame(string nome, double preco, int quantidade, string marca, string modelo, bool isUsado)
            :base(nome, preco, quantidade)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.IsUsado = isUsado;
        }
        public void CalcularImposto()
        {
            double tot = 0;
            string usado = "";
            if(IsUsado){
                tot = Preco * 0.25;
                usado = " usado";
            }else{
                tot = Preco * 0.45;
            }
            Console.WriteLine($"Imposto {Nome} {Modelo}{usado}, R$ {tot.ToString("N2", CultureInfo.InvariantCulture)}");
        }
    }
}