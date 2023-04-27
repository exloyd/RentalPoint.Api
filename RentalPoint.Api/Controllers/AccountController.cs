using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentalPoint.Authentication;
using RentalPoint.Data.Entities;
using RentalPoint.Services.Commands;
using RentalPoint.Services.Queries;
using RentalPoint.ViewModels;

namespace RentalPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetAccessToken([FromBody] LoginViewModel viewModel)
        {
            var userModel = _mapper.Map<User>(viewModel);
            var user = await _mediator.Send(new AuthorizeByCredentialsQuery(userModel));
            if (user == null)
                return Unauthorized("Учетные данные пользователя введены неверно");

            if (user.Penalty != null && user.Penalty.Any() && user.Penalty.Sum(x => x.Paid) != user.Penalty.Sum(x => x.Amount))
                return BadRequest("Пользователь имеет непогашенные штрафы");

            var claims = GetClaims(user);
            
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                AuthenticationOptions.Issuer,
                AuthenticationOptions.Audience,
                notBefore: now,
                claims: claims.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthenticationOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                login = user.Login,
                role = user.Role.ToString(),
                name = user.Name,
                id = user.Id
            };

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterViewModel viewModel)
        {
            var userModel = _mapper.Map<User>(viewModel);
            var id = await _mediator.Send(new CreateCommand<User>(userModel));
            if (id == Guid.Empty)
                return BadRequest("Пользователь с таким email уже существует в системе");
            
            var user = await _mediator.Send(new AuthorizeByCredentialsQuery(userModel, true));
            if (user == null)
                return Unauthorized("Учетные данные пользователя введены неверно");

            var claims = GetClaims(user);
            
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                AuthenticationOptions.Issuer,
                AuthenticationOptions.Audience,
                notBefore: now,
                claims: claims.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthenticationOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                login = user.Login,
                role = user.Role.ToString(),
                name = user.Name,
                id = user.Id
            };

            return Ok(response);
        }

        private static ClaimsIdentity GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.GivenName, user.Name)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimTypes.Name, ClaimTypes.Role);
            return claimsIdentity;
        }
    }
}