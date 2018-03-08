using System;
using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;

namespace senai.spacekids.webapi.Controllers
{
    [Route("api/[controller]")]

    public class LoginController : Controller
    {
         
        private IBaseRepository<Login> _loginRepository;

        public LoginController(IBaseRepository<Login> loginRepository)
        {
            _loginRepository = loginRepository;
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
                return Ok($"Usuário {login.email} Atualizado Com sucesso.");
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