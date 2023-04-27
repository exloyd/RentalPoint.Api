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
    [Authorize]
    public class OrderController : ControllerBase 
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<Order>>());
            return Ok(items);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrderViewModel viewModel)
        {
            var item = _mapper.Map<CreateOrder>(viewModel);
            var id = await _mediator.Send(new CreateCommand<CreateOrder>(item));
            return Ok(id);
        }
        
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] UpdateOrderViewModel viewModel)
        {
            var item = _mapper.Map<Order>(viewModel);
            await _mediator.Send(new UpdateCommand<Order>(id, item));
            return Ok();
        }
    }
}