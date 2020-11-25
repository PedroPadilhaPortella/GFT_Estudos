using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FuncionariosWA.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FuncionariosWA.DTO
{
    public class VagaDTO
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "O Nome do Projeto é Obrigatório.")]
        public string Projeto { get; set; }


        [Required(ErrorMessage = "A descrição do Projto é Obrigatório.")]
        public string Descricao { get; set; }


        [Required(ErrorMessage = "O Código da Vaga é Obrigatório.")]
        public string CodigoDaVaga { get; set; }


        [Required(ErrorMessage = "A quantidade de Vagas é Obrigatória.")]
        public int QuantidadeDeVagas { get; set; }


        [Required]
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
        public DateTime AberturaDaVaga { get; set; }


        [Required]
        public int[] TecnologiaId { get; set; }
        public List<VagaTecnologia> VagaTecnologias { get; set; }
        public List<Tecnologia> Tecnologias { get; set; }
    }
}