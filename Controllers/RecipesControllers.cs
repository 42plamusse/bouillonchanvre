using Microsoft.AspNetCore.Mvc;
using BouillonChanvre.Data;
using BouillonChanvre.Models;
using System.Collections.Generic;
using System.Linq;

namespace BouillonChanvre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/recipes
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetRecipes()
        {
            return _context.Recipes.ToList();
        }

        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipe(int id)
        {
            var recipe = _context.Recipes.Find(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // POST: api/recipes
        [HttpPost]
        public ActionResult<Recipe> CreateRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        // PUT: api/recipes/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/recipes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            var recipe = _context.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
