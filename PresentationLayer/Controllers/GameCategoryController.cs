using BusinessLogicLayer.DTOs.GameCategoyDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCategoryController(IGameCategoryService categoryService) : ControllerBase
    {
        private readonly IGameCategoryService _categoryService = categoryService;

        [HttpPost("/GameCategory/add/")]
        public async Task<IActionResult> Post(AddGameCategoryDto gameCategoryDto)
        {
            try
            {
                await _categoryService.AddGameCategoryAsync(gameCategoryDto);
                return Ok("Successfully Added");
            }
            catch (GameCategoryException ex)
            {
                return BadRequest(ex.errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/GameCategory/getall/")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _categoryService.GetAllGamesCategoryAsync();
            return Ok(list);
        }

        [HttpGet("/GameCategory/get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _categoryService.GetGameCategoryByIdAsync(id);
                return Ok(result);
            }
            catch (GameCategoryException ex)
            {
                return BadRequest(ex.errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("/GameCategory/update/")]
        public async Task<IActionResult> Put(UpdateGameCategoryDto updateGameCategory)
        {
            try
            {
                await _categoryService.UpdateAsync(updateGameCategory);
                return Ok("GameCategory Updated");
            }
            catch (GameCategoryException ex)
            {
                return BadRequest(ex.errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("/GameCategory/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return Ok("Seccessfully deleted");
            }
            catch (GameCategoryException ex)
            {
                return BadRequest(ex.errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
