using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using senai.spacekids.domain.Contracts;
using senai.spacekids.domain.Entities;
using senai.spacekids.repository.Context;


namespace senai.spacekids.webapi.Controllers {
    [Route ("api/[controller]")]
    public class PaiController : Controller {
        private IBaseRepository<Login> _loginRepository;
        private IBaseRepository<Pai> _paiRepository;

        public PaiController (IBaseRepository<Login> loginRepository, IBaseRepository<Pai> paiRepository) {
            _loginRepository = loginRepository;
            _paiRepository = paiRepository;
        }

        [Route ("cadastrar")]
        [HttpPost]
        public IActionResult Cadastro ([FromBody] Pai pai) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            try {
                _paiRepository.Inserir (pai);
                return Ok ("Pai " + pai.Login.email + " " + pai.nome + " Cadastrado Com Sucesso.");
            } catch (Exception ex) {
                return BadRequest ("Erro ao cadastrar dados. " + ex.Message);
            }
        }

        [Route("deletar/{id}")]
        [HttpDelete]
        public IActionResult Deletar (int id) {

            try {
                 _paiRepository.Deletar (id);
                 _loginRepository.Deletar(id);
                return Ok ("excluido com sucesso");
            } catch (System.Exception e) {

                return BadRequest ("Erro ao deletar usuario " + e.Message);
            }

        }

        [Route("atualizar/{id}")]
        [HttpPut]
        public IActionResult Atualizar (int id, [FromBody] Pai pai) {
            
            try
            {
            var p = _paiRepository.BuscarPorId(id);
            p.nome = pai.nome;
            p.Login.email = pai.Login.email;
            p.Login.senha = pai.Login.senha;

            _paiRepository.Atualizar (p);
            return Ok($" {p.nome} atualizado com sucesso");
            }
            catch (System.Exception e)
            {
                
                return BadRequest($"Erro ao atualizar {pai.nome} "+e.Message);
            }
        }

    }
}