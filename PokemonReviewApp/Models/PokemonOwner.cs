namespace PokemonReviewApp.Models
{
    public class PokemonOwner  // join table many to many
    {
        public int PokemonId { get; set; }  
        public int OwnerId { get; set;}
        public Pokemon Pokemon { get; set; }
        public Owner Owner { get; set; }    

    }
}
