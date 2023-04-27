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
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<User>>());
            return Ok(items);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetByIdAsync(Guid id)
        {
            var item = await _mediator.Send(new GetByIdQuery<User>(id));
            if (item != null)
                return Ok(item);

            return NotFound($"Не найден пользователь с идентификатором {id}");
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrUpdateUserViewModel viewModel)
        {
            var item = _mapper.Map<User>(viewModel);
            var id = await _mediator.Send(new CreateCommand<User>(item));
            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] CreateOrUpdateUserViewModel viewModel)
        {
            var item = _mapper.Map<User>(viewModel);
            await _mediator.Send(new UpdateCommand<User>(id, item));
            return Ok();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteCommand<User>(id));
            return Ok();
        }
    }
}