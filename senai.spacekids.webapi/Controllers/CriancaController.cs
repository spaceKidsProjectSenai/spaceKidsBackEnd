using System;
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

        /// <summary>
        /// Cadastra uma crianca no banco de dados
        /// </summary>
        /// <param name="crianca">dados da crianca conforme criterios estabelecidos. Faz-se necessario receber objeto inteiro.</param>
        /// <returns>String irá informar qual o objeto será cadastrado.</returns>
        /// 
        
        [Route("cadastrar")]
        [HttpPost]
        public IActionResult Cadastro([FromBody] Crianca crianca) 
        {
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
               return Ok (_criancaRepository.Listar(new string[]{"Crianca"}));
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
                _criancaRepository.Atualizar(crianca);
                return Ok ("Atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar "+ex.Message);
            }
        }
        
    }
}