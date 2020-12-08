using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CryptSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using desafio.Data;
using desafio.Models;

namespace desafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController: ControllerBase
    {
        private readonly DataContext Database;

        public UsuariosController(DataContext database) {
            this.Database = database;
        }


        [HttpGet]
        public IActionResult GetUsuarios() {
            return Ok(Database.Usuarios.ToList());
        }



        /// <summary>
        ///Registrar/Cadastrar usuários, 
        ///Exemplo:  { "email": "pedro@gft.com", "senha": "pedro@gft.com" }
        /// </summary>
        [HttpPost("Registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            try{
                if(usuario.Email.Length <= 6 || usuario.Senha.Length <= 6){
                    Response.StatusCode = 401;
                    return new ObjectResult( new {msg = "O Email e a Senha precisam ter mais de 6 caracteres!"});
                }

                if(Database.Usuarios.Any(u => u.Email.Equals(usuario.Email))){
                    Response.StatusCode = 401;
                    return new ObjectResult( new {msg = "Este Email já está cadastrado com outra Conta!"});
                }

                usuario.Senha = Crypter.Sha256.Crypt(usuario.Senha);
                
                Database.Add(usuario);
                Database.SaveChanges();
                return Ok(new {msg = "Usuário Cadastrado com Sucesso!"});
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }




        /// <summary>
        ///Login de usuários, que retorna um Token de validação.
        ///Exemplo:  { "email": "pedro@gft.com", "senha": "pedro@gft.com" }
        /// </summary>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Usuario credenciais)
        {
            try {
                Usuario usuario = Database.Usuarios.First(user => user.Email.Equals(credenciais.Email));
                if(usuario != null){
                    if(Crypter.CheckPassword(credenciais.Senha, usuario.Senha)){
                        
                        string chaveDeSeguranca = "secret_invoice_key";
                        var chaveSimetrica  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                        var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                        var Claims = new List<Claim>();
                        Claims.Add(new Claim("id", usuario.Id.ToString()));
                        Claims.Add(new Claim("email", usuario.Email));
                        
                        var JWT = new JwtSecurityToken(
                            issuer: "notafiscalAPI",
                            expires: DateTime.Now.AddHours(1),
                            audience: "public_user",
                            signingCredentials: credenciaisDeAcesso,
                            claims: Claims
                        );

                        return Ok(new {msg = "Usuário Logado, geramos seu token de Autorização!", token = new JwtSecurityTokenHandler().WriteToken(JWT)});

                    }else{
                        Response.StatusCode = 401;
                        return new ObjectResult( new {msg = "Senha Incorreta! "});
                    }
                }else{
                    Response.StatusCode = 401;
                    return new ObjectResult( new {msg = $"Usuário com Email {credenciais.Email} não encontrado! "});
                }
            } catch(Exception e) {
                Response.StatusCode = 401;
                return new ObjectResult( new {msg = "Usuário não encontrado!", erro = e});
            }
        }
    }
}