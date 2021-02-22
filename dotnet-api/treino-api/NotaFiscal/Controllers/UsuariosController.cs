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
            try{
                if(usuario.Email.Length <= 6 || usuario.Senha.Length <= 6){
                    Response.StatusCode = 401;
                    return new ObjectResult( new {msg = "O Email e a Senha precisam ter mais de 6 caracteres!"});
                }

                if(Database.Usuarios.Any(u => u.Email.Equals(usuario.Email))){
                    Response.StatusCode = 401;
                    return new ObjectResult( new {msg = "Este Email já está cadastrado com outra Conta!"});
                }

                usuario.Senha = Crypter.MD5.Crypt(usuario.Senha);
                usuario.Role = "Employee";
                /* Admin and Employee*/
                
                Database.Add(usuario);
                Database.SaveChanges();
                return Ok(new {msg = "Usuário Cadastrado com Sucesso!"});
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
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
                    return new ObjectResult( new {msg = $"Usuário com Email {credenciais.Email} não encontrado! "});
                }
            } catch(Exception e) {
                Response.StatusCode = 401;
                return new ObjectResult( new {msg = "Usuário não encontrado!", erro = e});
            }
        }


        [HttpPost("Admin")]
        [AllowAnonymous]
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


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsuarios() {
            return Ok(Database.Usuarios.ToList());
        }


        [HttpPatch("Editar/Nivel")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditarRoles([FromBody] Usuario usuarioBody)
        {
            if(usuarioBody.Id > 0){
                try{
                    var usuario = Database.Usuarios.First(p => p.Id == usuarioBody.Id);
                    if (usuario != null) {
                        usuario.Role = usuarioBody.Role != null ? usuarioBody.Role : usuario.Role;
                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new { msg = $"Cargo do Usuário {usuarioBody.Id} atualizado com Sucesso!" });
                    } else {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Usuário não encontrado!" });
                    }
                } catch (Exception) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Usuário com Id {usuarioBody.Id} não encontrado!" });
                }
            } else {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = $"Usuário com Id {usuarioBody.Id} não encontrado!" });
            }
        }


        [HttpPatch("Editar/Senha")]
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult EditarSenha([FromBody] Usuario usuarioBody)
        {
            if(usuarioBody.Email != null){
                try{
                    var usuario = Database.Usuarios.First(p => p.Email.Equals(usuarioBody.Email));
                    if (usuario != null) {
                        if(usuarioBody.Senha != null){
                            
                            string NovaSenha = Crypter.MD5.Crypt(usuarioBody.Senha);
                            usuario.Senha = NovaSenha;

                            Database.SaveChanges();
                            Response.StatusCode = 200;
                            return new ObjectResult(new { msg = $"Senha do Usuário {usuarioBody.Email} atualizado com Sucesso!" });
                        }else{
                            Response.StatusCode = 200;
                            return new ObjectResult(new { msg = $"A senha não pode ser Atualizada!" });
                        }
                    } else {
                        Response.StatusCode = 404;
                        return new ObjectResult(new { msg = "Usuário não encontrado!" });
                    }
                } catch (Exception) {
                    Response.StatusCode = 404;
                    return new ObjectResult(new { msg = $"Usuário com Email {usuarioBody.Email} não encontrado!" });
                }
            } else {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = $"Usuário com Email {usuarioBody.Email} não encontrado!" });
            }
        }
    }
}