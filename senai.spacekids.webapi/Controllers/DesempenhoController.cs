using System;
using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;

namespace senai.spacekids.webapi.Controllers
{
    [Route("api/[controller]")]
    public class DesempenhoController : Controller
    {
        private IBaseRepository<Desempenho> _desempenhoRepository;

        public DesempenhoController(IBaseRepository<Desempenho> desempenhoRepository)
        {
            _desempenhoRepository = desempenhoRepository;
        }

    [Route("cadastrar")]
    [HttpPost]
    public IActionResult Cadastro([FromBody] Desempenho desempenho)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _desempenhoRepository.Inserir(desempenho);
                return Ok("Fase salvo com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar" + ex.Message);
            }
        }

        [Route("deletar/{id}")]
        [HttpDelete]

        public IActionResult Deletar(int id)
        {
            try 
            {
                _desempenhoRepository.Deletar(id);

                return Ok("Desempenho excluída com sucesso");
            }
            catch(System.Exception e)
            {
                return BadRequest("Erro ao deletar o desempenho"+e.Message);
            }
        }
        [Route("atualizar")]
        [HttpPut]
        public IActionResult Atualizar([FromBody] Desempenho desempenho)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _desempenhoRepository.Atualizar(desempenho);
                return Ok ("Atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar "+ex.Message);
            }
        }

        [Route("listar")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_desempenhoRepository.Listar(new string[]{"Desempenho"}));
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao listar dados. " + ex.Message);
            }
        }  
    }
}