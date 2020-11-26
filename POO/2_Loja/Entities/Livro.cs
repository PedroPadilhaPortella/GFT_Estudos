using System;
using System.Collections.Generic;
using System.Globalization;

namespace _2_Loja.Entities
{
    class Livro : Produto, Imposto
    {
        public string Autor { get; set; }
        public string Tema { get; set; }
        public int QuantidadeDePaginas { get; set; }
        public Livro(){}
        public Livro(string nome, double preco, int quantidade, string autor, string tema, int quantidadeDePaginas)
            :base(nome, preco, quantidade)
        {
            this.Autor = autor;
            this.Tema = tema;
            this.QuantidadeDePaginas = quantidadeDePaginas;
        }
        public void CalcularImposto()
        {
            double tot = 0;
            if(Tema.ToLower() == "educativo"){
                System.Console.WriteLine($"Livro educativo não tem imposto: {Nome}");
            }else{
                tot = Preco * 0.1;
                System.Console.WriteLine($"R$ {tot.ToString("F2", CultureInfo.InvariantCulture)} de impostos sobre o livro {Nome}");
            }
        }
    }
}