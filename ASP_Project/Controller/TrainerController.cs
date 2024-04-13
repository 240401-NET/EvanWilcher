using Microsoft.AspNetCore.Mvc;

namespace ASP_Project;
public class TrainerController : Controller{
    //public Trainer trainer;
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