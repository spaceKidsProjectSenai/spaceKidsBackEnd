using System.Collections.Generic;
using System.Linq;
using senai.spacekids.repository.Context;
using senai.spacekids.domain.Entities;
using senai.spacekids.domain.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace senai.spacekids.webapi.Controllers
{
    [Route("api/[controller]")]
    public class PaiController:Controller
    {
        private IBaseRepository<Login> _loginRepository;
        private IBaseRepository<Pai> _paiRepository;


        public PaiController(IBaseRepository<Login> loginRepository, IBaseRepository<Pai> paiRepository) 
        {
            _loginRepository = loginRepository;
            _paiRepository = paiRepository;
        }

        [HttpPost]
        public IActionResult Cadastro([FromBody] Pai pai, Login login) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _loginRepository.Inserir(login); 
            var a = _paiRepository.Inserir(pai);

            if(a > 0) {
             return Ok(pai.nome +" "+ login.email+" cadastrado com sucesso");
            }else {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Deletar(Pai p, Login l) {
            var pai = _paiRepository.Deletar(p);
            var login = _loginRepository.Deletar(l);
            if(pai > 0 && login > 0)   {
                return Ok(p.nome+" "+l.email+" excluidos com sucesso");
            } else {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Pai p, Login l) {

            var pai = _paiRepository.Atualizar(p);
            var login = _loginRepository.Atualizar(l);
            if(pai > 0 && login > 0) {
                return Ok("Atualizados com sucesso");
            } else {
                return BadRequest();
            }
        }
        
    }
}