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

        /// <summary>
        /// Efetua o cadastro da fase no sistema.
        /// </summary>
        /// <returns>Retorna uma lista de fases cadastradas.</returns>
       
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

        /// <summary>
        /// Deleta o cadastro da fase no sistema.
        /// </summary>
        /// <returns>Deleta a fase pelo Id.</returns>


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
        /// <summary>
        /// Realiza a busca da fase no sistema pelo Id.
        /// </summary>
        /// <returns>Busca a fase da criança pelo Id.</returns>

        
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