using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dto;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Shared;

namespace WebApi.Controller.src.Controllers
{
    public class UserController : CrudController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IUserService _userService; // not necessary if no extra method needed
        public UserController(IUserService baseService) : base(baseService)
        {
            _userService = baseService;
        }

      /*  [Authorize(Roles = "Admin")]*/
        [HttpPost("admin")]
        public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto dto)
        {
            return CreatedAtAction(nameof(CreateAdmin), await _userService.CreateAdmin(dto));
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll([FromQuery] QueryOptions queryOptions)
        {
            return Ok(await _userService.GetAll(queryOptions));
        }

        // [AllowAnonymous]
        // [Authorize(Policy = "EmailWhiteList")]
        // [Authorize(Policy = "Minimum18Years")]
        public override async Task<ActionResult<UserReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _userService.GetOneById(id));
        }
        public override async Task<ActionResult<UserReadDto>> UpdateOneById(Guid id, UserUpdateDto updated)
        {
            return Ok(await _userService.UpdateOneById(id, updated));
        }

    }
}