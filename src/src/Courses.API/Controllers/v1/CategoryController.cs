using Courses.Application.Services.Interfaces;
using Courses.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Busca todas as categorias de cursos
        /// </summary>        
        /// <returns>Busca todas as categorias de cursos</returns>
        /// <response code="200">Return categorias de cursos</response>
        /// <response code="404">Not found</response>       
        /// <response code="500">Internal server error</response> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            if (categories != null && categories.Any())
            {
                return Ok(categories);
            }

            return NoContent();
        }

        /// <summary>
        /// Busca categoria por id
        /// </summary>        
        /// <returns>Busca categoria por id</returns>
        /// <response code="200">Return categoria</response>
        /// <response code="404">Not found</response>       
        /// <response code="500">Internal server error</response> 
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category != null)
            {
                return Ok(category);
            }

            return NoContent();
        }
    }
}
