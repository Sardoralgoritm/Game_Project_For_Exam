using BusinessLogicLayer.DTOs.GameCategoyDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCategoryController(IGameCategoryService categoryService) : ControllerBase
    {
        private readonly IGameCategoryService _categoryService = categoryService;

        [HttpGet("/game-category/getall/")]
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

        [HttpGet("/GameCategory/paged/")]
        public async Task<IActionResult> GetPaged(int pageSize = 10, int pageNumber = 1)
        {
            var list = await _categoryService.GetPagedListAsync(pageNumber, pageSize);
            var metaData = new
            {
                list.TotalCount,
                list.PageSize,
                list.CurrentPage,
                list.HasNext,
                list.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(list.Data);
        }

        [HttpGet("/GameCategory/filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterParametrs parametrs)
        {
            var categories = await _categoryService.Filter(parametrs);

            var metaData = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.HasNext,
                categories.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(categories.Data);
        }

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
                return StatusCode(500, ex.InnerException);
            }
        }
    }
}
