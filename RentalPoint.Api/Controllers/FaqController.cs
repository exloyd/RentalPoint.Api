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
    public class FaqController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FaqController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faq>>> GetAllAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<Faq>>());
            return Ok(items);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrUpdateFaqViewModel viewModel)
        {
            var item = _mapper.Map<Faq>(viewModel);
            var id = await _mediator.Send(new CreateCommand<Faq>(item));
            return Ok(id);
        }
        
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CreateOrUpdateFaqViewModel viewModel)
        {
            var item = _mapper.Map<Faq>(viewModel);
            await _mediator.Send(new UpdateCommand<Faq>(id, item));
            return Ok();
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Faq>(id));
            return Ok();
        }
    }
}