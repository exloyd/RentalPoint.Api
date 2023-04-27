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
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<Category>>());
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category>> GetByIdAsync(Guid id)
        {
            var item = await _mediator.Send(new GetByIdQuery<Category>(id));
            if (item != null)
                return Ok(item);

            return NotFound($"Не найдена категория с идентификатором {id}");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrUpdateCategoryViewModel viewModel)
        {
            var item = _mapper.Map<Category>(viewModel);
            var id = await _mediator.Send(new CreateCommand<Category>(item));
            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CreateOrUpdateCategoryViewModel viewModel)
        {
            var item = _mapper.Map<Category>(viewModel);
            await _mediator.Send(new UpdateCommand<Category>(id, item));
            return Ok();
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Category>(id));
            return Ok();
        }
    }
}