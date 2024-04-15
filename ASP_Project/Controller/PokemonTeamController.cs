using Microsoft.AspNetCore.Mvc;

namespace ASP_Project;
public class PokemonTeamController : Controller{
    private List<PokemonTeam> pkmTeams = new List<PokemonTeam>();
    [HttpGet("/Index")]
    public string Index(){
        return "This is the Pokemon Team Controller.";
    }
    [HttpGet("/PKMTeam")]
    public string PKMTeam(){
        return "Here is a pokemon team.";
    }
    void LoadTeam(string _trainerName){
        //create dbContex to get Trainer by name
    }
    void SaveTeam(int _index){
        // save pkmTeams[_index] 
    }
    void SaveTeams(Trainer _trainer){
        // save all teams
    }

    // do more stuff for views
}