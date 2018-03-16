using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;

namespace senai.spacekids.webapi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    public class FaseController : Controller
    {
        private IBaseRepository<Fase> _faseRepository;
        
        public FaseController(IBaseRepository<Fase> faseRepository)
        {
            _faseRepository = faseRepository;
        }

        [Route("cadastrar")]
        [HttpPost]

        public IActionResult Cadastro([FromBody] Fase fase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _faseRepository.Inserir(fase);
                return Ok("Fase" + fase.nome);
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
                _faseRepository.Deletar(id);

                return Ok("fase excluída com sucesso");
            }
            catch(System.Exception e)
            {
                return BadRequest("Erro ao deletar fase"+e.Message);
            }
        }
        
        [Route("buscarId/{id}")]
        [HttpGet]
        public IActionResult BuscarPorId(int id) {
            try
            {
                _faseRepository.BuscarPorId(id);

                return Ok("Busca com sucesso");
            }
            catch (System.Exception e)
            {
                
                return BadRequest("Erro ao buscar"+e.Message);
            }
        }
    }
}