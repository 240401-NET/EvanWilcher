using Microsoft.AspNetCore.Mvc;
using ASP_Project.Model;
using ASP_Project.DB;

namespace ASP_Project.Cont;
[Route("api/[controller]")]
public class TrainerController : Controller{
    //public Trainer trainer;
    [HttpGet("/Index")]
    public string Index(){
        return "This is the Trainer Controller.";
    }
    void LoadTrainer(string _name){
        //create dbContex to get Trainer by name
    }
    void SavedTrainer(){
        // save this.trainer
    }

    // do more stuff for views
}