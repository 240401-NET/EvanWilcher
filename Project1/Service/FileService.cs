using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;

public static class FileService{
    private static Dictionary<string, Dictionary<string, Trainer.PokemonTeam>> trainerToTeamsDict = new();
    private static Dictionary<string, Dictionary<string, Trainer.PokemonTeam>>.KeyCollection TrainerKeys{
         get { return trainerToTeamsDict.Keys; }
         set {}
     }
    private static Dictionary<string, Trainer> nameToTrainierDict = new();
    private static Dictionary<string, Trainer>.KeyCollection NameKeys{
        get { return nameToTrainierDict.Keys; }
        set {} 
    }
    
    static FileService(){
        // if(!File.Exists(Globals.FILETEAMS))
        //     File.Create(Globals.FILETEAMS);
        if(!File.Exists(Globals.FILE))
            File.Create(Globals.FILE);
        // if(!File.Exists(Globals.LOGFILE + '_' + DateTime.Now))
        //     File.Create(Globals.LOGFILE);
    }
    public static void LoadData(){
        try{
            nameToTrainierDict = JsonSerializer.Deserialize<Dictionary<string, Trainer>>(File.ReadAllText(Globals.FILE));
                
        }catch(Exception eMsg){
            return;
        }
    }
    public static void SaveData(Trainer _trainer, Trainer _removedTrainer = null){
        if(_trainer != null){
            if (NameKeys.Contains(_trainer.name)){
                if (nameToTrainierDict[_trainer.name].team.name == _trainer.team.name)
                    nameToTrainierDict[_trainer.name] = _trainer;
                else {
                    Trainer.PokemonTeam tempTeam = nameToTrainierDict[_trainer.name].team;
                    tempTeam.name = _trainer.team.name;
                    nameToTrainierDict[_trainer.name].team = tempTeam;
                }
            }
            else
                nameToTrainierDict.Add(_trainer.name, _trainer);
        }
        
        if (_removedTrainer != null)
            nameToTrainierDict.Remove(_removedTrainer.name);  
        string DEBUG_json = JsonSerializer.Serialize(nameToTrainierDict);
        File.WriteAllText(Globals.FILE, JsonSerializer.Serialize(nameToTrainierDict));
    }
    public static void LoadDataTeams() {
        try{
            trainerToTeamsDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Trainer.PokemonTeam>>>(File.ReadAllText(Globals.FILETEAMS));
                
        }catch(Exception eMsg){
            return;
        }
    }
    public static void SaveDataTeams(Trainer _trainer){
        if (TrainerKeys.Contains(_trainer.name)){
            foreach (string pkmTeamName in trainerToTeamsDict[_trainer.name].Keys){
                if (_trainer.team.name == pkmTeamName)
                    trainerToTeamsDict[_trainer.name][pkmTeamName] = _trainer.team;
                else if(pkmTeamName == trainerToTeamsDict[_trainer.name].Keys.Last()){
                    trainerToTeamsDict[_trainer.name].Add(_trainer.team.name, _trainer.team);
                }
            }
        }
        else
            trainerToTeamsDict.Add(_trainer.name, new(){{_trainer.team.name, _trainer.team}});    
        string DEBUG_json = JsonSerializer.Serialize(trainerToTeamsDict);
        File.WriteAllText(Globals.FILETEAMS, JsonSerializer.Serialize(trainerToTeamsDict));
    }
    public static List<Trainer.PokemonTeam> GetTrainersTeamList(string _trainerName) => trainerToTeamsDict[_trainerName].Values.ToList();
    public static Trainer.PokemonTeam GetTrainersTeam(ref Trainer _trainer, string _teamName) => trainerToTeamsDict[_trainer.name][_teamName];
    public static Trainer GetTrainer(string _trainerName) => nameToTrainierDict[_trainerName];
    public static bool DoesTrainerPresistTeams(string? _trainerName) => TrainerKeys.Contains(FindTrainerByNameTeams(_trainerName));
    public static string FindTrainerByNameTeams(string? _trainerName){
        foreach (string name in TrainerKeys){
            if (name == _trainerName){
                return name;
            }
        }
        return "";
    }
    public static bool DoesTrainerPresist(string? _trainerName) => NameKeys.Contains(_trainerName);
}