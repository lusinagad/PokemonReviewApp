using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using AutoMapper;
using PokemonReviewApp.Models;
using PokemonReviewApp.Dto;
using System.Reflection.Metadata.Ecma335;

namespace CategoryController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)  // inject IcategoryRepository
        {
            this.categoryRepository = categoryRepository;   // using automapper 
            this.mapper = mapper;
        }
        [HttpGet]                 // get method read
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = mapper.Map<List<CategoryDto>>(categoryRepository.GetCategories());

            if (!ModelState.IsValid)         // form of validation
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("categoryId")]   // get/read method 
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemon(int categoryId)
        {
            if (!categoryRepository.CategoryExists(categoryId))
                return NotFound();
            var category = mapper.Map<PokemonDto>(categoryRepository.GetCategories());

            if (!ModelState.IsValid)         // form of validation
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemons = mapper.Map<List<PokemonDto>>(categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = categoryRepository.GetCategories().Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim()).FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var categoryMap = mapper.Map<Category>(categoryCreate);

            if (!categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("","Something went wrong while saving");
                return StatusCode(500,ModelState);
            }

            return Ok("Successfully created");
        }
       
        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]   
        [ProducesResponseType(204)]     // no content error  
        [ProducesResponseType(404)]   // notfound error
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
           
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);
            if (!categoryRepository.CategoryExists(categoryId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = mapper.Map<Category>(updatedCategory);

            if (!categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(203)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!categoryRepository.CategoryExists(categoryId))
            { 
                return NotFound();
            }
            var categoryToDelete = categoryRepository.GetCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }
            return NoContent();
        }
    }
}
