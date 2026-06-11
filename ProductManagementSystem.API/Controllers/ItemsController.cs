using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Application.DTOs.Item;
using ProductManagementSystem.Application.Interfaces.Services;

namespace ProductManagementSystem.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IValidator<CreateItemDto> _createValidator;
        private readonly IValidator<UpdateItemDto> _updateValidator;

        public ItemsController(IItemService itemService,
             IValidator<CreateItemDto> createValidator,
             IValidator<UpdateItemDto> updateValidator)
        {
            _itemService = itemService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _itemService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _itemService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var id = await _itemService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
     int id,
     UpdateItemDto dto)
        {
            var validationResult =
                await _updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _itemService.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteAsync(id);

            return NoContent();
        }
    }
}
