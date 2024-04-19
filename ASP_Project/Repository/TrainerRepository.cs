using ASP_Project.DB;
using ASP_Project.Model;
namespace ASP_Project.Repository;
public class TrainerRepository : ITrainerRepo
{
    public readonly PokemonTrainerDbContext context;

    public TrainerRepository(PokemonTrainerDbContext _context){
        context = _context;
    }
    public object? GetByID(int _id){
        Trainer temp = context.Trainers.Find(_id);
        temp.PokemonTeams = context.PokemonTeams.Where(p => p.TrainerId == temp.Id).ToList();
        return temp;
    }
    public IEnumerable<object> GetAll(){
        return context.Trainers.ToList();
    }
    public object? DeleteByID(int _id){
        Trainer? tempTrainer = (Trainer?)GetByID(_id);
        ICollection<PokemonTeam> pokemonTeams = context.PokemonTeams.Where(p => p.TrainerId == tempTrainer.Id).ToList();
        ICollection<Pokemon> pokemons = [];
        foreach(PokemonTeam pkmTeam in pokemonTeams){
            ICollection<Pokemon> tempMons = context.Pokemons.Where(p => p.TeamId == pkmTeam.Id).ToList();
            foreach(Pokemon mon in tempMons)
                pokemons.Add(mon);
        }
        context.Pokemons.RemoveRange(pokemons);
        context.PokemonTeams.RemoveRange(pokemonTeams);
        context.Trainers.Remove(tempTrainer);
        context.SaveChanges();
        return tempTrainer;
    }
    public Trainer? EditByID(int _id, string name){
        Trainer? tempTrainer = (Trainer?)GetByID(_id);
        if (tempTrainer == null)
            return null;
        if (name == "")
            return tempTrainer;
        tempTrainer.Name = name;
        context.SaveChanges();
        return tempTrainer;
    }
    public Trainer? CreateNew(string _trainerName)
    {  
        Trainer returnTrainer = new(){
            Name = _trainerName
        };
        context.Trainers.Add(returnTrainer);
        context.SaveChanges();
        return returnTrainer;
    }
    public object? GetByName(string _name) => context.Trainers.Where(p => p.Name.Equals(_name, StringComparison.OrdinalIgnoreCase));
    public bool IsNameUsed(string _name) => context.Trainers.Count(p => p.Name == _name.Trim()) > 0 ? true : false;
    public bool DoesTrainerExist(int _id) => context.Trainers.Count(p => p.Id == _id) > 0 ? true : false;

}