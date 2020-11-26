using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CryptSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotaFiscal.Data;
using NotaFiscal.Models;

namespace NotaFiscal.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsuariosController: ControllerBase
    {
        private readonly NotaFiscalContext Database;

        public UsuariosController(NotaFiscalContext database) {
            this.Database = database;
        }

        [HttpPost("Registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            if(usuario.Email.Length <= 6 || usuario.Senha.Length <= 6){
                Response.StatusCode = 401;
                return new ObjectResult( new {msg = "O Email e a Senha precisam ter mais de 6 caracteres!"});
            }

            if(!(Database.Usuarios.First(u => u.Email.Equals(usuario.Email)) == null)){
                Response.StatusCode = 401;
                return new ObjectResult( new {msg = "Este Email já está cadastrado com outra Conta!"});
            }

            usuario.Senha = Crypter.MD5.Crypt(usuario.Senha);
            usuario.Role = "Employee";
            /* Admin and Employee*/
            
            Database.Add(usuario);
            Database.SaveChanges();
            return Ok(new {msg = "Usuário Cadastrado com Sucesso!"});
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Usuario credenciais)
        {
            try {
                Usuario usuario = Database.Usuarios.First(user => user.Email.Equals(credenciais.Email));
                if(usuario != null){
                    if(Crypter.CheckPassword(credenciais.Senha, usuario.Senha)){
                        
                        string chaveDeSeguranca = "secret_invoice_key";//Chave de Segurança
                        var chaveSimetrica  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca)); //chave simétrica
                        var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature); //credencial de acesso

                        var Claims = new List<Claim>(); //Claims só trabalham com strings
                        Claims.Add(new Claim("id", usuario.Id.ToString()));
                        Claims.Add(new Claim("email", usuario.Email));
                        Claims.Add(new Claim(ClaimTypes.Role, usuario.Role));
                        
                        var JWT = new JwtSecurityToken(
                            issuer: "notafiscalAPI", //quem está fornecendo o JWT para o usuário
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
                    return new ObjectResult( new {msg = "Usuário não encontrado! "});
                }
            } catch {
                Response.StatusCode = 401;
                return new ObjectResult( new {msg = "Usuário não encontrado! "});
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsuarios() => Ok(Database.Usuarios.ToList());

        [HttpPost("Admin")]
        public IActionResult SeedMainUser(){
            string role = "Admin";
            if(Database.Usuarios.Any(user => user.Role == role)) {
                Response.StatusCode = 401;
                return new ObjectResult( new {msg = "Usuário Administrator já cadastrado!"});
            }

            Usuario mainUser = new Usuario();
            mainUser.Email = "pedro.kadjin.sg@gmail.com";
            string Senha = "GFT@pedro2020";
            mainUser.Senha = Crypter.MD5.Crypt(Senha);
            mainUser.Role = "Admin";
            Database.Add(mainUser);
            Database.SaveChanges();
            return Ok(new {msg = "Administrator Cadastrado, os dados de Acesso são:", mainUser});
        }
    }
}