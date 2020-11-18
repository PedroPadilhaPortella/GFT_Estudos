using System.Linq;
using System;
using FuncionariosWA.Data;
using FuncionariosWA.Models;
using FuncionariosWA.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FuncionariosWA.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext Database;

        public ServiceController(ApplicationDbContext database)
        {
            Database = database;
        }

        public IActionResult SeedData()
        {
            if (!(Database.LocaisDeTrabalho.Any()))
            {
                Database.AddRange(
                    new LocalDeTrabalho(1, "Alphaville", "06454-000", "Alameda Rio Negro, n° 585 Ed. Padauiri, 10° andar", "Barueri", "São Paulo", "55 11 2176-3253", true),
                    new LocalDeTrabalho(2, "Curitiba", "80230-010", "Av. Sete de Setembro, 2451 Torre Trinity Corporate 6º andar", "Curitiba", "Paraná", "55 41 4009-5700", true),
                    new LocalDeTrabalho(3, "Sorocaba", "18095-450", "Av. São Francisco, 98 Jardim Sta. Rosália", "Sorocaba", "São Paulo", "55 11 2176-3253", true)
                );
            }
            if (!(Database.Tecnologias.Any()))
            {
                Database.AddRange(
                    new Tecnologia(1, "Asp.Net Core", true),
                    new Tecnologia(2, "Node.JS", true),
                    new Tecnologia(3, "AngularJS", true),
                    new Tecnologia(4, "React", true),
                    new Tecnologia(5, "React Native", true),
                    new Tecnologia(6, "Javascript", true),
                    new Tecnologia(7, "AWS", true),
                    new Tecnologia(8, "PHP", true),
                    new Tecnologia(9, "Django.Py", true),
                    new Tecnologia(10, "Machine Learning", true),
                    new Tecnologia(11, "SQL", true),
                    new Tecnologia(12, "MongoDB", true),
                    new Tecnologia(13, "Java", true),
                    new Tecnologia(14, "Android", true),
                    new Tecnologia(15, "Kotlin", true),
                    new Tecnologia(16, "Web", true),
                    new Tecnologia(17, "Scrum", true),
                    new Tecnologia(18, "Infraestrutura e Redes", true),
                    new Tecnologia(19, "Gestão de Negócios", true),
                    new Tecnologia(20, "UX", true)
                );
            }
            if (!(Database.Cargos.Any()))
            {
                Database.AddRange(
                    new Cargo(1, "Desenvolvedor.NET", true),
                    new Cargo(2, "Desenvolvedor Java", true),
                    new Cargo(3, "Desenvolvedor RPA", true),
                    new Cargo(4, "Desenvolvedor Node.js", true),
                    new Cargo(5, "Desenvolvedor Mobile", true),
                    new Cargo(6, "Analista de QA", true),
                    new Cargo(7, "Analista / Desenvolvedor Salesforce", true),
                    new Cargo(8, "Gerente", true),
                    new Cargo(9, "Desenvolvedor FullStack", true),
                    new Cargo(10, "Estagiário", true),
                    new Cargo(11, "Gestor de Projetos", true),
                    new Cargo(12, "Analista de Sistemas", true),
                    new Cargo(13, "Cientista de Dados", true),
                    new Cargo(14, "Especialista em UX", true),
                    new Cargo(15, "Técnico de Infraestrutura", true)
                );
            }

            Database.SaveChanges();
            return RedirectToAction("SeedData2", "Service");
        }

        public IActionResult SeedData2()
        {
            if (!(Database.Vagas.Any()))
            {
                Database.AddRange(
                    new Vaga(1, "Santander Mobile", "Projetar Aplicação Mobile para Migração do Aplicativo do Banco Santander com ReactNative", "SAN-MB-001", 6, new DateTime(2020, 08, 12), Database.Cargos.First(c => c.Id == 5), true),
                    new Vaga(2, "Bradesco WebApp", "Desenvolver Aplicação Web para Gestão de Contas do Banco Bradesco com Asp.Net Core", "BRA-WA-023", 3, new DateTime(2020, 10, 09), Database.Cargos.First(c => c.Id == 1), true),
                    new Vaga(3, "Deustch Bank Remote", "Projeto Híbrido de Expansão do Deustch Bank no Brasil, projetando novas métricas e objetivos", "DEU-BK-007", 2, new DateTime(2021, 02, 22), Database.Cargos.First(c => c.Id == 11), true),
                    new Vaga(4, "Vivo WebApp", "Desenvolver Aplicação Web para Clientes da Vivo com Hibernate e RestAPI Java", "VVO-WA-013", 8, new DateTime(2020, 12, 19), Database.Cargos.First(c => c.Id == 13), true),
                    new Vaga(5, "Santander Mobile", "Desenvolvedor UX para Aplicação Mobile do Banco Santander", "SAN-MB-002", 3, new DateTime(2020, 08, 12), Database.Cargos.First(c => c.Id == 14), true),
                    new Vaga(6, "GFT_START UNI", "Instrutores para o Programa de Estagio GFT START", "GFT-ST-006", 2, new DateTime(2020, 10, 14), Database.Cargos.First(c => c.Id == 12), true),
                    new Vaga(7, "GFT_START UNI", "Estagiários da GFT_START Uni", "GFT-ST-040", 40, new DateTime(2020, 10, 14), Database.Cargos.First(c => c.Id == 10), true)
                );
            }
            if (!(Database.Funcionarios.Any()))
            {
                Database.AddRange(
                    new Funcionario(1, "Pedro Henrique Padilha Portella", "102020001", new DateTime(2020, 10, 14), Database.Cargos.First(c => c.Id == 10),  Database.LocaisDeTrabalho.First(l => l.Id == 1), true),
                    new Funcionario(2, "Clécio Gomes da Rocha Silva", "092016012", new DateTime(2020, 04, 29), Database.Cargos.First(c => c.Id == 12),  Database.LocaisDeTrabalho.First(l => l.Id == 1), true),
                    new Funcionario(3, "Francielle de Jesus", "102020006", new DateTime(2021, 06, 02), Database.Cargos.First(c => c.Id == 4), Database.LocaisDeTrabalho.First(l => l.Id == 3), true),
                    new Funcionario(4, "Edwin Samuel Padilha Portella", "102020045", new DateTime(2021, 05, 30), Database.Cargos.First(c => c.Id == 9), Database.LocaisDeTrabalho.First(l => l.Id == 2), true),
                    new Funcionario(5, "Daniel Padilha Portella", "09102021026", new DateTime(2028, 11, 10), Database.Cargos.First(c => c.Id == 13), Database.LocaisDeTrabalho.First(l => l.Id == 2), true)
                );
            }

            Database.SaveChanges();
            return RedirectToAction("SeedData3", "Service");
        }

        public IActionResult SeedData3()
        {
            if(!(Database.VagaTecnologias.Any()))
            {
                Database.AddRange(
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 1), Database.Tecnologias.First(t => t.Id == 5)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 1), Database.Tecnologias.First(t => t.Id == 6)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 1), Database.Tecnologias.First(t => t.Id == 12)),

                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 2), Database.Tecnologias.First(t => t.Id == 1)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 2), Database.Tecnologias.First(t => t.Id == 16)),

                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 3), Database.Tecnologias.First(t => t.Id == 19)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 3), Database.Tecnologias.First(t => t.Id == 17)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 3), Database.Tecnologias.First(t => t.Id == 13)),

                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 4), Database.Tecnologias.First(t => t.Id == 13)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 4), Database.Tecnologias.First(t => t.Id == 11)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 4), Database.Tecnologias.First(t => t.Id == 16)),

                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 5), Database.Tecnologias.First(t => t.Id == 20)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 5), Database.Tecnologias.First(t => t.Id == 19)),

                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 6), Database.Tecnologias.First(t => t.Id == 17)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 6), Database.Tecnologias.First(t => t.Id == 16)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 6), Database.Tecnologias.First(t => t.Id == 13)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 6), Database.Tecnologias.First(t => t.Id == 1)),

                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 7), Database.Tecnologias.First(t => t.Id == 1)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 7), Database.Tecnologias.First(t => t.Id == 13)),
                    new VagaTecnologia(Database.Vagas.First(v => v.Id == 7), Database.Tecnologias.First(t => t.Id == 16))
                );

                Database.AddRange(
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 1), Database.Tecnologias.First(t => t.Id == 1)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 1), Database.Tecnologias.First(t => t.Id == 2)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 1), Database.Tecnologias.First(t => t.Id == 6)),

                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 2), Database.Tecnologias.First(t => t.Id == 1)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 2), Database.Tecnologias.First(t => t.Id == 7)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 2), Database.Tecnologias.First(t => t.Id == 16)),

                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 3), Database.Tecnologias.First(t => t.Id == 16)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 3), Database.Tecnologias.First(t => t.Id == 12)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 3), Database.Tecnologias.First(t => t.Id == 8)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 3), Database.Tecnologias.First(t => t.Id == 18)),

                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 4), Database.Tecnologias.First(t => t.Id == 13)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 4), Database.Tecnologias.First(t => t.Id == 14)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 4), Database.Tecnologias.First(t => t.Id == 15)),

                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 5), Database.Tecnologias.First(t => t.Id == 9)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 5), Database.Tecnologias.First(t => t.Id == 12)),
                    new FuncionarioTecnologia(Database.Funcionarios.First(t => t.Id == 5), Database.Tecnologias.First(t => t.Id == 11))
                );
            }

            Database.SaveChanges();
            return RedirectToAction("Wa", "Wa");
        }
    }
}