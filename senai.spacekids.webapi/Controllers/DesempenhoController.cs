using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;

namespace senai.spacekids.webapi.Controllers {
    [Route ("api/[controller]")]
    [EnableCors ("AllowAnyOrigin")]
    public class DesempenhoController : Controller {
        private IBaseRepository<Desempenho> _desempenhoRepository;

        public DesempenhoController (IBaseRepository<Desempenho> desempenhoRepository) {
            _desempenhoRepository = desempenhoRepository;
        }

        /// <summary>
        /// Efetua o cadastro do desempenho da criança no sistema.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / cadastrar
        ///     {
        ///        "acertou" : true, 
        ///        "horaInicial": "2018-04-07 17:45:43",
        ///        "horaFinal": "2018-04-07 19:45:43",
        ///        "criancaId" : 7,
        ///        "faseId" : 2
        ///     }
        ///
        /// </remarks>
        /// <returns>Retorna o cadastro de desempenho da criança.</returns>
        [Route ("cadastrar")]
        [HttpPost]
        public IActionResult Cadastro ([FromBody] Desempenho desempenho) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            try {
                _desempenhoRepository.Inserir (desempenho);
                return Ok ("Fase salvo com sucesso");
            } catch (Exception ex) {
                return BadRequest ("Erro ao cadastrar" + ex.Message);
            }
        }

    
        /// <summary>
        /// Cria uma lista com os cadastros desempenho das crianças no sistema.
        /// </summary>
        /// <returns>Retorna uma lista de desempenho da criança.</returns> 
        [Route ("listar")]
        [HttpGet]
        public IActionResult Listar () {
            try {
                return Ok (_desempenhoRepository.Listar ());
            } catch (SystemException ex) {
                return BadRequest ($"Erro ao listar fases." + ex.Message);
            }

        }
    
        /// <summary>
        /// Lista as crianças por Id.
        /// </summary>
        /// <returns>Retorna uma lista de crianças usando o id.</returns> 
        [Route ("listar/{id}")]
        [HttpGet ("{id}")]
        public IActionResult ListarPorIdCrianca (int id) {
            try {
                return Ok (_desempenhoRepository.Listar().Where(x => x.criancaId == id));
            } catch (SystemException ex) 
            {
                return BadRequest ($"Erro ao listar fases." + ex.Message);
            }

        }
}
}