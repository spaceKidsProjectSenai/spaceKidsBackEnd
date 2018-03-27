using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        /// <summary>
        /// Cadastra e realiza login do pai no banco de dados
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / Login
        ///     {
        ///        "email": email@email.com,
        ///        "senha": "1234",
        ///     }
        ///
        /// </remarks>
        /// <param name="pai">dados do pai conforme criterios estabelecidos. Faz-se necessario receber objeto inteiro.</param>
        /// <returns>String irá informar qual o objeto será cadastrado.</returns>   
        [Route("autenticar")]
        [HttpPost]
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
                        new Claim(ClaimTypes.Email, log.email),
                        new Claim("userId", log.LoginId.ToString()),
                        new Claim(ClaimTypes.Role, log.Permissao)
                    }
                );

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
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
        /// <summary>
        /// Efetua o cadastro do pai no sistema.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / cadastrar
        ///     {
        ///        "nome" : "fulano de tal"    
        ///        "email": "email@email.com",
        ///        "senha": "1234",
        ///     }
        ///
        /// </remarks>
        /// <returns>Retorna uma lista de pais cadastrados.</returns>
        [Route("cadastrar")]
        [HttpPost]
        public IActionResult Cadastro([FromBody] Login login) 
         
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                login.Permissao = "Pai";
                _loginRepository.Inserir(login);
                return Ok(login);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao acessar. " + ex.Message);
            }            
        }

        /// <summary>
        /// Deleta o cadastro do pai no sistema.
        /// </summary>
        /// <returns>Deleta o cadastro de um pai pelo Id.</returns>
  
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

        /// <summary>
        /// Efetua a atualização dos dados do login do pai.
        /// </summary>
        /// <returns>Retorna um login atualizado.</returns> 

        [Route("atualizar")]
        [HttpPut]
        public IActionResult Atualizar([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Login login_ = _loginRepository.BuscarPorId(login.LoginId);

                if(login == null)
                    return NotFound("Login não encontrado");

                login_.email = login.email;
                login_.nome = login.nome;
                login_.Permissao = login.Permissao;

                _loginRepository.Atualizar(login_);
                return Ok(login_);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar dados " + ex.Message);
            }

        }  

        /// <summary>
        /// Cria uma lista com os cadastros dos pais no sistema.
        /// </summary>
        /// <returns>Retorna uma lista de pais cadastrados.</returns>     
 
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