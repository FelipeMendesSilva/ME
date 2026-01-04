using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ME.SuperHero.API.Controllers
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
        public async Task<ActionResult<IEnumerable<ResponseGetHeroi>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(new RequestGetAllHerois(), cancellationToken);
            if (!result.IsSuccess())
                return NotFound(result.ErrorMessage);
            
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseGetHeroi>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(new RequestGetHeroiById(id), cancellationToken);
            if (!result.IsSuccess())
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateHeroi request, CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(request, cancellationToken);
            if (!result.IsSuccess())
                return BadRequest(result.ErrorMessage);

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] RequestUpdateHeroi request, CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(request, cancellationToken);
            if (!result.IsSuccess())
            {
                if(result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return NotFound(result.ErrorMessage);
                return BadRequest(result.ErrorMessage);
            }
                
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(new RequestDeleteHeroi(id), cancellationToken);
            if (!result.IsSuccess())
            {
                if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return NotFound(result.ErrorMessage);
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet("superpoderes")]
        public async Task<ActionResult<IEnumerable<ResponseGetHeroi>>> GetAllSuperpoderesAsync(CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(new RequestGetSuperpoderes(), cancellationToken);
            if (!result.IsSuccess())
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
