using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;

namespace senai.spacekids.webapi.Controllers
{
    [Route("api/[controller]")]
    public class CriancaController:Controller
    {
        private IBaseRepository<Crianca> _criancaRepository;

        public CriancaController(IBaseRepository<Crianca> criancaRepository) {
            _criancaRepository = criancaRepository;
        }

        [Route("cadastrar")]
        [HttpPost]
        public IActionResult Cadastro([FromBody] Crianca crianca) {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                _criancaRepository.Inserir(crianca);
                return Ok($"Criança cadastrada {crianca.nome}");
            }
            catch (System.Exception e)
            {
                
                return BadRequest($"Erro ao cadastrar jogador {e}");
            }
        }

        [Route("listar")]
        [HttpGet]
        public IActionResult Listar() {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            try
            {
               return Ok (_criancaRepository.Listar(new string[]{"Crianca"}));
            }
            catch (System.Exception e)
            {
                return BadRequest($"Erro ao listar crianças {e}");
                
            }
        }
        
    }
}