using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;

namespace api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext Database;

        public UsuariosController(ApplicationDbContext database) {
            this.Database = database;
        }
        [HttpPost("Registro")]
        public IActionResult Registros([FromBody] Usuario usuario){
            //Verificar se as credenciais são válidas
            //Verificar se o email já está cadastrado no banco em outra conta
            //Encriptar senha
            
            Database.Add(usuario);
            Database.SaveChanges();
            return Ok(new {msg = "Usuário Cadastrado com Sucesso!"});
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Usuario credenciais){
            //Buscar um usuario por email
            //Verificar se a senha está correta
            //Gerar um token JWT e retornar esse tolken para o usuário
            try{
                Usuario usuario = Database.Usuarios.First(user => user.Email.Equals(credenciais.Email));
                if(usuario != null){
                    //um usuario com esse email foi encontrado
                    if(usuario.Senha.Equals(credenciais.Senha)){
                        //Senha validada, login aprovado
                        //Gerar Token JWT
                        
                        string chaveDeSeguranca = "securitykeypedroportella";//Chave de Segurança
                        var chaveSimetrica  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca)); //chave simétrica
                        var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature); //credencial de acesso

                        var Claims = new List<Claim>(); //Claims só trabalham com strings
                        Claims.Add(new Claim("id", usuario.Id.ToString()));
                        Claims.Add(new Claim("email", usuario.Email));
                        Claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        
                        var JWT = new JwtSecurityToken(
                            issuer: "PedroPortellaAPI", //quem está fornecendo o JWT para o usuário
                            expires: DateTime.Now.AddHours(1),
                            audience: "usuario_comum",
                            signingCredentials: credenciaisDeAcesso,
                            claims: Claims
                        );

                        return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));

                    }else{
                        Response.StatusCode = 401;
                        return new ObjectResult( new {msg = "Senha Incorreta! "});
                    }
                }else{
                    //não há nenhum usuário com esse email
                    Response.StatusCode = 401;
                    return new ObjectResult( new {msg = "Usuário não encontrado! "});
                }
            }catch {
                //não há nenhum usuário com esse email
                Response.StatusCode = 401;
                return new ObjectResult( new {msg = "Usuário não encontrado! "});
            }
        }
    }
}