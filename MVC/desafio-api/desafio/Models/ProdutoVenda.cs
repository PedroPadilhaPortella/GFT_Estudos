namespace desafio.Models
{
    public class ProdutoVenda
    {
        public int Quantidade { get; set; }
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }
        public Venda Venda { get; set; }
        public Produto Produto { get; set; }

        public ProdutoVenda() { }
        public ProdutoVenda(int quantidade, Venda venda, Produto produto)
        {
            this.Quantidade = quantidade;
            this.Venda = venda;
            this.Produto = produto;
        }
    }
}