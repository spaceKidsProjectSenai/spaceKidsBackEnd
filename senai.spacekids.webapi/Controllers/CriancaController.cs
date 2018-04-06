using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;

namespace senai.spacekids.webapi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    public class CriancaController:Controller
    {
        private IBaseRepository<Crianca> _criancaRepository;

        public CriancaController(IBaseRepository<Crianca> criancaRepository) {
            _criancaRepository = criancaRepository;
        }

        /// <summary>
        /// Cadastra uma crianca no banco de dados
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /cadastrar
        ///     {
        ///        "nome": "nome da criança",
        ///        "idade": 4,
        ///        "sexo": "feminino",
        ///        "foto": "url da foto da criança"
        ///     }
        /// </remarks>
        /// <returns>String irá informar qual o objeto será cadastrado.</returns>
        
        [Route("cadastrar")]
        [HttpPost]
        [Authorize("Bearer", Roles = "Pai")]
        public IActionResult Cadastro([FromBody] Crianca crianca) 
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                List<Claim> claim = HttpContext.User.Claims.ToList();

                var userId = claim.FirstOrDefault(c => c.Type == "userId").Value;
                crianca.loginId = int.Parse(userId);
                _criancaRepository.Inserir(crianca);
                return Ok(crianca);
            }
            catch (System.Exception ex)
            {
                var retorno = new {
                    autenticacao = false,
                    message = $"Erro ao cadastrar jogador {ex}"
                };
                return BadRequest(retorno);
                //return BadRequest($"Erro ao cadastrar jogador {e}");
            }
        }

        /// <summary>
        /// Lista as criancas que foram cadastradas.
        /// </summary>
        /// <returns>Retorna uma lista das criancas cadastradas.</returns>

        [Route("listar")]
        [HttpGet]
        public IActionResult Listar() 
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            try
            {
               return Ok (_criancaRepository.Listar());
            }
            catch (System.Exception e)
            {
                return BadRequest($"Erro ao listar crianças {e}");
                
            }
        }

        /// <summary>
        /// Lista as criancas que foram cadastradas.
        /// </summary>
        /// <returns>Retorna uma lista das criancas cadastradas.</returns>

        [Route("listarporpai")]
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult ListarPorPai() 
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<Claim> claim = HttpContext.User.Claims.ToList();

                var userId = claim.FirstOrDefault(c => c.Type == "userId").Value;

               return Ok (_criancaRepository.Listar().Where(x => x.loginId.ToString() == userId));
            }
            catch (System.Exception e)
            {
                return BadRequest($"Erro ao listar crianças {e}");
                
            }
        }

        /// <summary>
        /// Deleta uma crianca cadastrada.
        /// </summary>
        /// <param name="id">A crianca sera deletada a partir do id.</param>
        /// <returns>Retorna o objeto deletado.</returns>

        [Route("deletar/{id}")]
        [HttpDelete]

        public IActionResult Deletar(int id)
        {
            try 
            {
                _criancaRepository.Deletar(id);

                return Ok("Desempenho excluída com sucesso");
            }
            catch(System.Exception e)
            {
                return BadRequest("Erro ao deletar o desempenho"+e.Message);
            }
        }

        /// <summary>
        /// Atualiza as informacoes da crianca cadastrada.
        /// </summary>
        /// <param name="crianca">As informacoes da crianca serao atualizadas.</param>
        /// <returns>A string retorna as informacoes atualizadas.</returns>

        [Route("atualizar")]
        [HttpPut]
        public IActionResult Atualizar([FromBody] Crianca crianca)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Crianca crianca_ = _criancaRepository.BuscarPorId(crianca.CriancaId);

                if(crianca_ == null)
                    return NotFound("Criança não encontrada");

                crianca_.nome = crianca.nome;
                crianca_.idade = crianca.idade;
                crianca_.sexo = crianca.sexo;
                crianca_.foto = crianca.foto;

                _criancaRepository.Atualizar(crianca_);
                return Ok (crianca_);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar "+ex.Message);
            }
        }
        
    }
}