using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroiController : ControllerBase
    {
        private readonly ILogger<HeroiController> _logger;
        private readonly IMediator _mediatr;

        public HeroiController(ILogger<HeroiController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediatr = mediator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetHeroiResponse>> GetAll()
        {
            return Ok();
        }
        
        [HttpGet("{id}")]
        public ActionResult<GetHeroiResponse> GetById(int id)
        {
            //var hero = _heroes.FirstOrDefault(h => h.Id == id);
            //if (hero == null)
            //    return NotFound();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateHeroi heroi, CancellationToken cancellationToken)
        {
            var createdHeroi = await _mediatr.Send(heroi, cancellationToken);
           
            return CreatedAtAction(nameof(GetById), new { id = 123 }, heroi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] RequestUpdateHeroi heroAtualizado, CancellationToken cancellationToken)
        {
            //var hero = _heroes.FirstOrDefault(h => h.Id == id);
            //if (hero == null)
            //    return NotFound();

            //hero.Nome = heroAtualizado.Nome;
            //hero.Poder = heroAtualizado.Poder;
            //hero.Cidade = heroAtualizado.Cidade;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var hero = _heroes.FirstOrDefault(h => h.Id == id);
            //if (hero == null)
            //    return NotFound();

            //_heroes.Remove(hero);
            return NoContent();
        }
    }
}
