using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPoint.Data.Entities;
using RentalPoint.Data.Models;
using RentalPoint.Services.Queries;

namespace RentalPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<MonthlyReport<Order>>>> GetMonthlyOrdersAsync()
        {
            var items = await _mediator.Send(new GetMonthlyReportQuery<IEnumerable<MonthlyReport<Order>>>());
            return Ok(items);
        }
        
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<MonthlyReport<User>>>> GetMonthlyUsersAsync()
        {
            var items = await _mediator.Send(new GetMonthlyReportQuery<IEnumerable<MonthlyReport<User>>>());
            return Ok(items);
        }
        
        [HttpGet("statistics")]
        public async Task<ActionResult<IEnumerable<StatisticsReport>>> GetStatisticsAsync()
        {
            var items = await _mediator.Send(new GetListQuery<IEnumerable<StatisticsReport>>());
            return Ok(items);
        }
    }
}