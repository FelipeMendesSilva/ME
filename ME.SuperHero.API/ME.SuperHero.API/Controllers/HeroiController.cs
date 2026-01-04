using ME.SuperHero.Application.Requests;
using ME.SuperHero.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ME.SuperHero.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroisController : ControllerBase
    {
        private readonly ILogger<HeroisController> _logger;
        private readonly IMediator _mediatr;

        public HeroisController(ILogger<HeroisController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediatr = mediator;
        }

        /// <summary> 
        ///  Obtém todos os heróis cadastrados. 
        /// </summary> 
        [HttpGet] 
        [ProducesResponseType(typeof(IEnumerable<ResponseGetHeroi>), StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ResponseGetHeroi>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(new RequestGetAllHerois(), cancellationToken);
            if (!result.IsSuccess())
                return NotFound(result.ErrorMessage);
            
            return Ok(result.Data);
        }

        /// <summary> 
        /// Obtém um herói específico pelo seu identificador. 
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseGetHeroi), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseGetHeroi>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(new RequestGetHeroiById(id), cancellationToken);
            if (!result.IsSuccess())
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary> 
        /// Cria um novo herói. 
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateHeroi request, CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(request, cancellationToken);
            if (!result.IsSuccess())
                return BadRequest(result.ErrorMessage);

            return Created();
        }

        /// <summary> 
        /// Atualiza os dados de um herói existente. 
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary> 
        /// Remove um herói pelo seu identificador. 
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary> 
        /// Obtém todos os superpoderes. 
        /// </summary>
        [HttpGet("Superpoderes")]
        [ProducesResponseType(typeof(IEnumerable<ResponseGetHeroi>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ResponseGetHeroi>>> GetAllSuperpoderesAsync(CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(new RequestGetSuperpoderes(), cancellationToken);
            if (!result.IsSuccess())
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
