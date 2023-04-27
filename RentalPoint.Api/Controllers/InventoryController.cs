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
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InventoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAllAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<Inventory>>());
            var mappedItems = _mapper.Map<IEnumerable<InventoryViewModel>>(items);
            return Ok(mappedItems);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Inventory>> GetByIdAsync(Guid id)
        {
            var item = await _mediator.Send(new GetByIdQuery<Inventory>(id));
            if (item != null)
            {
                var mappedItem = _mapper.Map<InventoryViewModel>(item);
                return Ok(mappedItem);
            }
            
            return NotFound($"Не найден инвентарь с идентификатором {id}");
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrUpdateInventoryViewModel viewModel)
        {
            var item = _mapper.Map<Inventory>(viewModel);
            var id = await _mediator.Send(new CreateCommand<Inventory>(item));
            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CreateOrUpdateInventoryViewModel viewModel)
        {
            var item = _mapper.Map<Inventory>(viewModel);
            await _mediator.Send(new UpdateCommand<Inventory>(id, item));
            return Ok();
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Inventory>(id));
            return Ok();
        }
    }
}