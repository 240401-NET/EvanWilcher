using ASP_Project.Repository;
using ASP_Project.Model;
using ASP_Project.Controller.Exceptoins;

namespace ASP_Project.Service;

public class PokemonTeamServices : IPKMTeamService{
    private readonly IPKMTeamRepo pkmTeamRepo;
    private readonly IRESTPKMService restPKMService;
    public PokemonTeamServices(IPKMTeamRepo _pkmTeamRepo, IRESTPKMService _restPKMService) {
        pkmTeamRepo = _pkmTeamRepo;
        restPKMService = _restPKMService;
    } 
    public object? GetByID(int _id) => pkmTeamRepo.GetByID(_id);
    public object? DeleteByID(int _id) => pkmTeamRepo.DeleteByID(DoesTrainerExist(_id));
    public IEnumerable<object> GetAll() => pkmTeamRepo.GetAll();
    public PokemonTeam? CreateNew(int _trainerID, string _pkmTeamName) => pkmTeamRepo.CreateNew(DoesTrainerExist(_trainerID), new(){Name = ValidateName(_pkmTeamName)});    
    public PokemonTeam? EditByID(int _id, string _name = "", IEnumerable<string>? _pokemonNames = null) => pkmTeamRepo.EditByID(DoesTeamExist(_id), ValidateName(_name), FetchPokemon(_pokemonNames));
    public PokemonTeam? EditByID(int _id, string _name = "", int _teamPosition = 0, string? _pokemonName = null ) => pkmTeamRepo.EditByID(DoesTeamExist(_id), ValidateName(_name), IsPositionInBounds(_teamPosition), FetchPokemon(_pokemonName));
    public IEnumerable<PokemonTeam> GetByName(string _pkmTeamName) => (IEnumerable<PokemonTeam>)pkmTeamRepo.GetByName(ValidateName(_pkmTeamName));
    private string ValidateName(string _name){
        if (_name == null){
            throw new NullReferenceException($"Client Pokemon team name was null");
        }
        _name = _name.Trim();
        if (_name.Contains(Globals.CHARACTERINVALIDATION))
            throw new BadNameException($"Trainer has a bad name.  Do not use thise characters: \n{Globals.CHARACTERINVALIDATION}");
        return _name;
    }
    private string ValidatePokemonName(string _name){
        if (_name == null){
            throw new NullReferenceException($"Client Pokemon name name was null");
        }
        _name = _name.Trim();
        if (_name.Contains(Globals.CHARACTERINVALIDATION) ||
            _name == "")
            throw new BadNameException($"Pokemon has a bad name.  Do not use thise characters: \n{Globals.CHARACTERINVALIDATION}");
        return _name;
    }
    private int IsPositionInBounds(int _teamPosition) => _teamPosition >= 0 && _teamPosition < Globals.MAXPKMTEAMSIZE ? _teamPosition : throw new IndexOutOfRangeException("Team position is out of bounds, please choose positions 0 - 5");
    private int DoesTrainerExist(int _id) => pkmTeamRepo.DoesTrainerExist(_id) ? _id : throw new NullReferenceException("Trainer does not exist.");
    private int DoesTeamExist(int _id) => pkmTeamRepo.DoesTeamExist(_id) ? _id : throw new NullReferenceException("Team does not exist."); 
    private Pokemon FetchPokemon(string? _pkmName){
        if (_pkmName == null)
            throw new NullReferenceException("Pokemon name is null.\nPlease provide a list of Pokemon.");
        Pokemon? tempMon = restPKMService.GetPokemonData(ValidatePokemonName(_pkmName.ToLower()));
        return tempMon == null ? throw new NullReferenceException($"{_pkmName} does not exist.\nPlease select a different pokemon.") : tempMon;
    }
    private IEnumerable<Pokemon> FetchPokemon(IEnumerable<string>? _pokemonNames){
        if (_pokemonNames == null)
            throw new NullReferenceException("List of pokemon is null.\nPlease provide a list of Pokemon.");
        if (_pokemonNames.Count() >= Globals.MAXPKMTEAMSIZE){
            throw new TeamSizeException($"The team is too big.  Only choose {Globals.MAXPKMTEAMSIZE} pokemon.");
        }
        List<Pokemon> tempList = new();
        foreach(string name in _pokemonNames){
            Pokemon? tempPokemon = restPKMService.GetPokemonData(ValidatePokemonName(name.ToLower()));
            if (tempPokemon == null)
                throw new NullReferenceException($"{name} does not exist.\nPlease select a different pokemon.");
            tempList.Add(tempPokemon);
        }
        return tempList;
    }   
}