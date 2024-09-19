using ApiPontuacao.Models;
using ApiPontuacao.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiPontuacao.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PessoaController : Controller
    {
            private readonly ServicePessoa _service;
            public PessoaController(ServicePessoa service)
            {
                _service = service;
            }



            // GET: api/v1/pessoas
            [HttpGet]
            public IActionResult Get()
            {

                return Ok(_service.GetAll());
            }

        // GET: api/v1/pessoas/{id}
        [HttpGet("{id}")] // Correção aqui
        public IActionResult Get(int id)
        {
            try
            {
                return StatusCode(200, _service.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/v1/pessoas
        [HttpPost]
            public IActionResult Post([FromBody] Pessoa pessoa)
            {

                try
                {
                    return Ok(_service.Save(pessoa));
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }


            }

            // PUT: api/v1/pessoas/{id}
            [HttpPut("/{id}")]
            public IActionResult Put(int id, [FromBody] Pessoa pessoaAtualizada)
            {
                try
                {
                    _service.Update(pessoaAtualizada);
                    return StatusCode(200, "Pessoa alterada com sucesso");
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }

            }



            // DELETE: api/pessoas/{id}
            [HttpDelete("/{id}")]
            public IActionResult Delete(int id)
            {
                try
                {
                    _service.Delete(id);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }

            }
        }
        
}
