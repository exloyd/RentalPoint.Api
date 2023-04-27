using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalPoint.Data.Entities;
using RentalPoint.Services.Commands;
using RentalPoint.Services.Queries;
using RentalPoint.ViewModels;

namespace RentalPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PenaltyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PenaltyController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Penalty>>> GetAllAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<Penalty>>());
            return Ok(items);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreatePenaltyViewModel viewModel)
        {
            var item = _mapper.Map<Penalty>(viewModel);
            var id = await _mediator.Send(new CreateCommand<Penalty>(item));
            return Ok(id);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] UpdatePenaltyViewModel viewModel)
        {
            var item = _mapper.Map<Penalty>(viewModel);
            await _mediator.Send(new UpdateCommand<Penalty>(id, item));
            return Ok();
        }
    }
}