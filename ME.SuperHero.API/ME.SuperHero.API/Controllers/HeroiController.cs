using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroiController : ControllerBase
    {
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
        public IActionResult Create([FromBody] RequestCreateHeroi heroi)
        {
            //hero.Id = _heroes.Count > 0 ? _heroes.Max(h => h.Id) + 1 : 1;
            //_heroes.Add(hero);
            return CreatedAtAction(nameof(GetById), new { id = heroi.Id }, heroi);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RequestUpdateHeroi heroAtualizado)
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
