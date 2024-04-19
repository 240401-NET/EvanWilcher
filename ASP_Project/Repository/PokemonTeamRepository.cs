using System.Reflection.Metadata.Ecma335;
using ASP_Project.Controller;
using ASP_Project.DB;
using ASP_Project.Model;

namespace ASP_Project.Repository;
public class PokemonTeamRepository : IPKMTeamRepo
{
    public readonly PokemonTrainerDbContext context;

    public PokemonTeamRepository(PokemonTrainerDbContext _context){
        context = _context;
    }
    public object? GetByID(int _id){
        PokemonTeam temp = context.PokemonTeams.Find(_id);
        temp.Trainer = context.Trainers.Find(temp.TrainerId);
        temp.Pokemons = context.Pokemons.Where(p => p.TeamId == temp.Id).ToList();
        return temp;
    }
    public PokemonTeam? EditByID(int _id, string _name, IEnumerable<Pokemon> _pokemons){
        PokemonTeam? tempPKM = (PokemonTeam?)GetByID(_id);
        if (tempPKM == null)
            return null;
        foreach(Pokemon mon in _pokemons){
            mon.Team = tempPKM;
            mon.TeamId = _id;
        }
        tempPKM.Name = _name == "" ? tempPKM.Name : _name;
        tempPKM.Pokemons = _pokemons.ToList();
        context.SaveChanges();
        return tempPKM;
    }
    public PokemonTeam? EditByID(int _id, string name, int _teamPosition, Pokemon _pokemon)
    {
        PokemonTeam? tempPKM = (PokemonTeam?)GetByID(_id);
        if (tempPKM == null)
            return null;
        _pokemon.Team = tempPKM;
        _pokemon.TeamId = _id;
        if (tempPKM.Pokemons.Count >= Globals.MAXPKMTEAMSIZE)
            tempPKM.Pokemons.ToList()[_teamPosition] = _pokemon;
        else
            tempPKM.Pokemons.Add(_pokemon);
        tempPKM.Name = (name == "") ? tempPKM.Name : name;
        context.SaveChanges();
        return tempPKM;
    }

    public PokemonTeam? CreateNew(int _trainerID, PokemonTeam _pkmTeam)
    {
        Trainer? tempTrainer = context.Trainers.Find(_trainerID);
        if (tempTrainer == null)
            return null;
        _pkmTeam.TrainerId = _trainerID;
        _pkmTeam.Trainer = tempTrainer;
        context.PokemonTeams.Add(_pkmTeam);
        context.SaveChanges();
        return _pkmTeam;
    }

    public object? DeleteByID(int _id)
    {
        PokemonTeam? tempPKM = (PokemonTeam?)GetByID(_id);
        ICollection<Pokemon> pokemons = context.Pokemons.Where(p => p.TeamId == tempPKM.Id).ToList();
        context.Pokemons.RemoveRange(pokemons);
        context.PokemonTeams.Remove(tempPKM);
        context.SaveChanges();
        return tempPKM;
    }

    public IEnumerable<object> GetAll()
    {
        IEnumerable<PokemonTeam> returnTeams = context.PokemonTeams.ToList();
        foreach (PokemonTeam team in returnTeams){
            team.Trainer = context.Trainers.Find(team.TrainerId);
            team.Pokemons = context.Pokemons.Where(p => p.TeamId == team.Id).ToList();
        }
        return context.PokemonTeams.ToList();   
    }

    public object? GetByName(string _name)
    {
        ICollection<PokemonTeam> returnTeams = context.PokemonTeams.Where(p => p.Name == _name).ToList();
        foreach (PokemonTeam team in returnTeams){
            team.Trainer = context.Trainers.Where(p => p.Id == team.TrainerId).Single();
            team.Pokemons = context.Pokemons.Where(p => p.TeamId == team.Id).ToList();
        }
        return returnTeams;
    }

    public bool DoesTrainerExist(int _id) => context.Trainers.Count(p => p.Id == _id) > 0 ? true : false;
    public bool IsNameUsed(string _name) => context.PokemonTeams.Count(p => p.Name == _name) > 0 ? true: false;
    public bool DoesTeamExist(int _id) => context.PokemonTeams.Count(p => p.Id == _id) > 0 ? true : false;
}