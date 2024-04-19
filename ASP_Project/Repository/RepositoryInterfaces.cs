using ASP_Project.Model;
namespace ASP_Project.Repository;
public interface IPokeRepo{
    public object? GetByID(int _id);
    public object? DeleteByID(int _id);
    public IEnumerable<object> GetAll();
    public object? GetByName(string _name);
    public bool IsNameUsed(string _name);
    public bool DoesTrainerExist(int _id);
}
public interface IPKMTeamRepo:IPokeRepo{
    public PokemonTeam? CreateNew(int _trainerID, PokemonTeam _pkmTeam);
    public PokemonTeam? EditByID(int _id, string _name, IEnumerable<Pokemon>? _pokemons);
    public PokemonTeam? EditByID(int _id, string name, int _teamPosition, Pokemon? _pokemon);
    public bool DoesTeamExist(int _id);
}
public interface ITrainerRepo:IPokeRepo{
    public Trainer? CreateNew(string _trainerName);
    public Trainer? EditByID(int _id, string name);
}