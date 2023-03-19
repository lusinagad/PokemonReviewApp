using AutoMapper;
using AutoMapper.Configuration;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Data;


namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext context;
      
        public CountryRepository(DataContext context)
        {
            this.context = context;           
        }

        public bool CreateCountry(Country country)
        {
            context.Add(country);
            return Save();
        }

        public bool CountryExists(int id)
        {
           return context.Countries.Any(c=>c.Id == id); 
        }

        public ICollection<Country> GetCountries()
        {
            return context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return context.Countries.Where(e => e.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            return context.Owners.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;          
        }

        public bool UpdateCountry(Country country)
        {
            context.Update(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            context.Remove(country);
            return Save();
        }
    }   
}
