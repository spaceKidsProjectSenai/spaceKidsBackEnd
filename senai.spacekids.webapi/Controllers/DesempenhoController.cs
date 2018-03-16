using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;

namespace senai.spacekids.webapi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
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
    }
}