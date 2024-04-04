
// See https://aka.ms/new-console-template for more information

 // Grand Rising 
Console.WriteLine("\\o  Hello Trainer!  o/");
// Constants
const string DISPLAYBADNAMEERRORMESSAGE = "Did not find the pokemon, Please ensure the Pokemon's name is correct.";

// quit the console application
void Quit(){
    Console.WriteLine("Goodbye Trainer!");
    Environment.Exit(0);
}


// run forever...
while (true)
{
    // heads up user!
    Console.WriteLine("What pokemon would you like to search?\nEnter 'q' to quit.");
    string? userInput = Console.ReadLine();
    // q for quit.
    if("q".Equals(userInput, StringComparison.CurrentCultureIgnoreCase)){
        Quit();
    }
    // clear the screen
    // TODO::Wrapper function to presist information on the screen
    //       after clearing the screen
    Console.Clear();
    // GOTTA GET'EM ALL! (or just the one the user asked for)
    Pokemon? userPokemon = RESTService.GetPokemonData(userInput.ToLower());
    //Error handling and a good candidate for try/catch block 
    Console.WriteLine((userInput == null|| userInput =="") ? DISPLAYBADNAMEERRORMESSAGE : 
                                                            (userPokemon == null ? DISPLAYBADNAMEERRORMESSAGE:
                                                                                   userPokemon.ToString()));
    //run forever...
    while (true){
        // heads up user!
        Console.WriteLine("Want to search again? Enter Y or N");
        userInput = Console.ReadLine();
        // clear the screen.
        Console.Clear();
        // do it again
        if ("y".Equals(userInput, StringComparison.CurrentCultureIgnoreCase))
        {
            break;
        }
        // n for... quit
        else if ("n".Equals(userInput, StringComparison.CurrentCultureIgnoreCase)){
           Quit();
        }
    }
    
}
