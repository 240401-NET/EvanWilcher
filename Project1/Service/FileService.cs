
using System.Text.Json;

public static class FileService{
    private static Dictionary<Trainer, List<Trainer.PokemonTeam>> trainerToTeamsDict = new();
    private static Dictionary<Trainer, List<Trainer.PokemonTeam>>.KeyCollection Keys{
         get { return trainerToTeamsDict.Keys; }
         set {}
     }
    public static string FILE{
        get { return "SavedTeamsByTrainers.json"; }
        set {}
    } 
    public static void LoadData(){
        string savedData = File.ReadAllText(FILE);
        trainerToTeamsDict = JsonSerializer.Deserialize<Dictionary<Trainer, List<Trainer.PokemonTeam>>>(savedData);
    }    
    public static void SaveData(Trainer _trainer){
        string a = _trainer.name;
    }
}