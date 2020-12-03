using AutoMapper;
using desafio.DTO;
using desafio.Models;

namespace desafio.Data
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Produto, ProdutoAuxiliar>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();
            CreateMap<Fornecedor, FornecedorAuxiliar>().ReverseMap();
            CreateMap<Venda, VendaDTO>().ReverseMap();
            CreateMap<ProdutoVenda, ProdutoVendaDTO>().ReverseMap();
        }
    }
}