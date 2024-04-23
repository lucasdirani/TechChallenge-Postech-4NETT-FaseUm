using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Controllers
{
    [Route("contacts")]
    [ApiController]
    public class ContactController: ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return null;
            //return await _mediator.Send(request);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactInput input)
        {            
            return (IActionResult)await _mediator.Send(input);
        }
    }
}
