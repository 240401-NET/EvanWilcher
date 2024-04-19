using Microsoft.AspNetCore.Mvc;
using ASP_Project.Model;
using ASP_Project.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using ASP_Project.Controller.Exceptoins;

namespace ASP_Project.Controller;
[Route("api/[controller]")]
[ApiController]
public class TrainerController : ControllerBase{

    private readonly ITrainerService trainerService;

    public TrainerController(ITrainerService _trainerService) => trainerService = _trainerService;

    //public Trainer trainer;
    [HttpPost("/NewTrainer/{_trainerName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Trainer> CreateTrainer(string _trainerName){
        try{
            return Ok(trainerService.CreateNew(_trainerName));
        }catch (NullReferenceException _e){
            return BadRequest(_e.Message);
        }catch (BadNameException _e){
            return BadRequest(_e.Message);
        }
    }
    
    [HttpDelete("/Trainer/Remove/{_id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Trainer> DeleteTrainer(int _id){
        try{
            return Ok(trainerService.DeleteByID(_id));
        }catch (NullReferenceException _e){
            return BadRequest(_e.Message);
        }
    }

    [HttpPatch("/Trainer/{_id}&{_name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Trainer> EditTrainer(int _id, string _name){
        try{
            return Ok(trainerService.EditByID(_id, _name));
        }catch (NullReferenceException _e){
            return BadRequest(_e.Message);
        }catch (BadNameException _e){
            return BadRequest(_e.Message);
        }
    }

    [HttpGet("/Trainers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Trainer>> GetAll(){
        try{
            return Ok(trainerService.GetAll());
        }catch (Exception _e){
            return BadRequest(_e.Message);
        }
    }

    [HttpGet("/Trainer/{_id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Trainer> GetTrainerByID(int _id){
        try{
            return Ok(trainerService.GetByID(_id));
        }catch (NullReferenceException _e){
            return BadRequest(_e.Message);
        }
    }
    // do more stuff for views
}