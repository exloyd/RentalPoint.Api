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
    public class PromoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PromoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promo>>> GetAllAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<Promo>>());
            return Ok(items);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Promo>> GetByIdAsync(Guid id)
        {
            var item = await _mediator.Send(new GetByIdQuery<Promo>(id));
            if (item != null)
                return Ok(item);

            return NotFound($"Не найдена акция с идентификатором {id}");
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrUpdatePromoViewModel viewModel)
        {
            var item = _mapper.Map<Promo>(viewModel);
            var id = await _mediator.Send(new CreateCommand<Promo>(item));
            return Ok(id);
        }
        
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CreateOrUpdatePromoViewModel viewModel)
        {
            var item = _mapper.Map<Promo>(viewModel);
            await _mediator.Send(new UpdateCommand<Promo>(id, item));
            return Ok();
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Promo>(id));
            return Ok();
        }
    }
}