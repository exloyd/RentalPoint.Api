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
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NewsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetAllAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<News>>());
            var itemsViewModel = _mapper.Map<IEnumerable<NewsViewModel>>(items);
            return Ok(itemsViewModel);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category>> GetByIdAsync(Guid id)
        {
            var item = await _mediator.Send(new GetByIdQuery<News>(id));
            if (item != null)
                return Ok(item);

            return NotFound($"Не найдена новость с идентификатором {id}");
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrUpdateNewsViewModel viewModel)
        {
            var item = _mapper.Map<News>(viewModel);
            var id = await _mediator.Send(new CreateCommand<News>(item));
            return Ok(id);
        }
        
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CreateOrUpdateNewsViewModel viewModel)
        {
            var item = _mapper.Map<News>(viewModel);
            await _mediator.Send(new UpdateCommand<News>(id, item));
            return Ok();
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteCommand<News>(id));
            return Ok();
        }
    }
}