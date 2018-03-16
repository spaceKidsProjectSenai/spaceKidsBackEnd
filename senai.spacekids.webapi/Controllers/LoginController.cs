using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;
using senai.spacekids.repository.Repositories;

namespace senai.spacekids.webapi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]

    public class LoginController : Controller
    {
         
        private IBaseRepository<Login> _loginRepository;

        public LoginController(IBaseRepository<Login> loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Validar([FromBody] Login login, [FromServices] SigningConfigurations signingConfigurations, [FromServices] TokenConfigurations tokenConfigurations)
        {
            Login log = _loginRepository.Listar().FirstOrDefault(c => c.email == login.email && c.senha == login.senha);
            if (log != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(login.LoginId.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, log.LoginId.ToString()),
                        new Claim(ClaimTypes.Email, log.email)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity
                });

                var token = handler.WriteToken(securityToken);
                var retorno = new
                {
                    autenticacao = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    acessToken = token,
                    message = "Ok"
                };

                return Ok(retorno);
            }

            var retornoerro = new
            {
                autenticacao = false,
                message = "Falha na Autenticação"
            };

            return BadRequest(retornoerro);
            
        }      
       
        [Route("cadastrar")]
        [HttpPost]
        public IActionResult Cadastro([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _loginRepository.Inserir(login);
                return Ok("Login " + login.email + " Login validado");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao acessar. " + ex.Message);
            }            
        }

        [Route("deletar/{id}")]
        [HttpDelete]
        public IActionResult Deletar(int id)
        {

            try
            {
                _loginRepository.Deletar(id);

                return Ok("login excluido com sucesso");
            }
            catch (System.Exception e)
            {

                return BadRequest("Erro ao deletar login " + e.Message);
            }

        }

        [Route("atualizar")]
        [HttpPut]
        public IActionResult Atualizar([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _loginRepository.Atualizar(login);
                return Ok($"Usuário {login.nome} Atualizado Com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar dados " + ex.Message);
            }

        }  

        [Route("listar")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_loginRepository.Listar(new string[]{"Login"}));
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao listar dados. " + ex.Message);
            }
        }      
    }
}