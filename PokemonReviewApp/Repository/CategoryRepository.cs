using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

// repository its just where to put all database calls

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;
       
        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }
        public bool CreateCategory(Category category) // create / post method
        {
            // change tracker 
            // add, updating, modifying, connected vs disconnected
            // EntitiyState.Added 

            context.Add(category);
           // context.SaveChanges(); // kam espes kam stexcel aranzin method aranzin 
            return Save();

        }
        public bool CategoryExists(int id)
        {
            return context.Categories.Any(c => c.Id == id); 
        }

        public ICollection<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return context.Categories.Where(e =>e.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return context.PokemonCategories.Where(e => e.Category.Id == categoryId).Select(c => c.Pokemon).ToList();
        }
    
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            context.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            context.Remove(category);
            return Save();
        }
    }   
}
