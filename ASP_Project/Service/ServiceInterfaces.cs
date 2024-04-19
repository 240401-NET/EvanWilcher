using ASP_Project.Model;
namespace ASP_Project.Service;
public interface IPokeService{
    public object? GetByID(int _id);
    public object? DeleteByID(int _id);
    public IEnumerable<object> GetAll();
}
public interface IPKMTeamService:IPokeService{
    public PokemonTeam? CreateNew(int _trainerID, string _pkmTeamName);
    public PokemonTeam? EditByID(int _id, string _name = "", IEnumerable<string>? _pokemonNames = null);
    public PokemonTeam? EditByID(int _id, string _name = "", int _teamPosition = 0, string? _pokemonNames = null );
    public IEnumerable<PokemonTeam> GetByName(string _pkmTeamName);
}
public interface ITrainerService:IPokeService{
    public Trainer? CreateNew(string _trainerName);
    public Trainer? EditByID(int _id, string _name);
}

public interface IRESTPKMService{
    public Pokemon? GetPokemonData(string? _pokemonName);
    public PokemonHeldItem? GetItem(string? _itemName);
}