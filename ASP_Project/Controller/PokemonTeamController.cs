using Microsoft.AspNetCore.Mvc;
using ASP_Project.Service;
using ASP_Project.Model;
using ASP_Project.Controller.Exceptoins;

namespace ASP_Project.Controller;
[Route("api/[controller]")]
[ApiController]
public class PokemonTeamController : ControllerBase{
    private readonly IPKMTeamService pkmTeamService;
    public PokemonTeamController(IPKMTeamService _pkmTeamService) => pkmTeamService = _pkmTeamService;

    [HttpPost("/NewTeam/{_trainerID}&{_pkmTeamName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PokemonTeam> CreatePKMTeam(int _trainerID, string _pkmTeamName){
        try{
            return Ok(pkmTeamService.CreateNew(_trainerID, _pkmTeamName));
        }catch (NullReferenceException _e){
            return BadRequest(_e.Message);
        }catch (BadNameException _e){
            return BadRequest(_e.Message);
        }
    }

    [HttpGet("/Team/{_id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PokemonTeam> GetPKMTeam(int _id){
        try{
            return Ok(pkmTeamService.GetByID(_id));
        }catch(Exception _e){
            return BadRequest(_e.Message);
        }
    }

    [HttpDelete("/Team/Remove/{_id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PokemonTeam> DeletePKMTeam(int _id){
        try{
            return Ok(pkmTeamService.DeleteByID(_id));
        }catch(NullReferenceException _e){
            return BadRequest(_e.Message);
        }
    }

    [HttpGet("/Teams")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetAllTeams(){
        try{
            return Ok(pkmTeamService.GetAll());
        }catch(Exception _e){
            return BadRequest(_e.Message);
        }
    }
    
    [HttpPatch("/Team/Full/{_id}&{_name}&{_pokemonNames}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PokemonTeam> EditPKMTeamByID(int _id, string _name = "", IEnumerable<string>? _pokemonNames = null){
        try{
            return Ok(pkmTeamService.EditByID(_id, _name, _pokemonNames));
        }catch (NullReferenceException _e){
            return BadRequest(_e.Message);
        }catch (BadNameException _e){
            return BadRequest(_e.Message);
        }
    }

    [HttpPatch("/Team/Single/{_id}&{_name}&{_teamPosition}&{_pokemonName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PokemonTeam> EditPKMTeamByID(int _id, string _name = "", int _teamPosition = 0, string? _pokemonName = null){
        try{
            return Ok(pkmTeamService.EditByID(_id, _name, _teamPosition, _pokemonName));
        }
        catch(IndexOutOfRangeException _e){
            return BadRequest(_e.Message); 
        }catch (NullReferenceException _e){
            return BadRequest(_e.Message);
        }catch (BadNameException _e){
            return BadRequest(_e.Message);
        }
    }
    [HttpGet("/Teams/{_pkmTeamName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetPKMTeamsByName(string _pkmTeamName){
        try{
                return Ok(pkmTeamService.GetByName(_pkmTeamName));
        }catch (BadNameException _e){
            return BadRequest(_e);
        }
    }
}