namespace PokemonReviewApp.Dto;


// limits the model  resieve and send data .
public class PokemonDto
{ 
    public int Id { get; set; }   // models is database tables 
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
}
