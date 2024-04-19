using ASP_Project.Controller.Exceptoins;
using ASP_Project.Model;
using ASP_Project.Repository;

namespace ASP_Project.Service;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepo trainerRepo;
    public TrainerService(ITrainerRepo _trainerRepo) => trainerRepo = _trainerRepo;
    public Trainer? CreateNew(string _trainerName) => trainerRepo.CreateNew(ValidateName(_trainerName));
    public object? DeleteByID(int _id) => trainerRepo.DeleteByID(DoesTrainerExist(_id));
    public Trainer? EditByID(int _id, string _name) => trainerRepo.EditByID(DoesTrainerExist(_id), ValidateName(_name));
    public IEnumerable<object> GetAll() => trainerRepo.GetAll();
    public object? GetByID(int _id){
        Trainer? returnTrainer = (Trainer)trainerRepo.GetByID(_id);
         return returnTrainer == null ? throw new NullReferenceException("Trainer does not exist.  Please use a different ID") : returnTrainer;
    } 
    private string ValidateName(string? _str){
        if (_str == null){
            throw new NullReferenceException($"Client trainer name was null");
        }
        _str = _str.Trim();
        if (_str== "" ||
            Globals.CHARACTERINVALIDATION.ToCharArray().Any(c => _str.Contains(c)))
            throw new BadNameException($"Trainer has a bad name.  Do not use thise characters: \n{Globals.CHARACTERINVALIDATION}");
        if (trainerRepo.IsNameUsed(_str))
            throw new BadNameException($"{_str} is already being used.\nPlease use a different name.");
        return _str;
    }
    private Trainer ValidateTrainer(Trainer? _trainer) => _trainer == null ? throw new NullReferenceException($"Something bad happened in the Data Base.\nWe will have the issue logged.") : _trainer;    
    private int DoesTrainerExist(int _id) => trainerRepo.DoesTrainerExist(_id) ? _id : throw new NullReferenceException("Trainer does not exist.");
}