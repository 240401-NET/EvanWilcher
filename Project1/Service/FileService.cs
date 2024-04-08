using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;

public static class FileService{
    private static Dictionary<string, Dictionary<string, Trainer.PokemonTeam>> trainerToTeamsDict = new();
    private static Dictionary<string, Dictionary<string, Trainer.PokemonTeam>>.KeyCollection TrainerKeys{
         get { return trainerToTeamsDict.Keys; }
         set {}
     }
    static FileService(){
        if(!File.Exists(Globals.FILE))
            File.Create(Globals.FILE);
        // if(!File.Exists(Globals.LOGFILE + '_' + DateTime.Now))
        //     File.Create(Globals.LOGFILE);
    }
    
    public static void LoadData() {
        try{
            trainerToTeamsDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Trainer.PokemonTeam>>>(File.ReadAllText(Globals.FILE));
                
        }catch(Exception eMsg){
            return;
        }
    }
    public static void SaveData(Trainer _trainer){
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
        File.WriteAllText(Globals.FILE, JsonSerializer.Serialize(trainerToTeamsDict));
    }
    public static List<Trainer.PokemonTeam> GetTrainersTeamList(string _trainerName) => trainerToTeamsDict[_trainerName].Values.ToList();
    public static Trainer.PokemonTeam GetTrainersTeam(ref Trainer _trainer, string _teamName) => trainerToTeamsDict[_trainer.name][_teamName];
    public static bool DoesTrainerPresist(string? _trainerName) => TrainerKeys.Contains(FindTrainerByName(_trainerName));
    public static string FindTrainerByName(string? _trainerName){
        foreach (string name in TrainerKeys){
            if (name == _trainerName){
                return name;
            }
        }
        return "";
    }
}