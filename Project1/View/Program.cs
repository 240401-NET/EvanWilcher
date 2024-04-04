
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

void Quit(){
    Console.WriteLine("Goodbye Trainer!");
    Environment.Exit(0);
}
const string DISPLAYBADNAMEERRORMESSAGE = "Did not find the pokemon, Please insure the Pokemon's name is correct.";

while (true)
{
    Console.WriteLine("What pokemon would you like to search?\nEnter 'q' to quit.");
    string? userInput = Console.ReadLine();
    if("q".Equals(userInput, StringComparison.CurrentCultureIgnoreCase)){
        Quit();
    }
    Console.Clear();
    Pokemon? userPokemon = ASPWrapper.GetPokemonData(userInput.ToLower());
    Console.WriteLine((userInput == null|| userInput =="") ? DISPLAYBADNAMEERRORMESSAGE : 
                                                            (userPokemon == null ? DISPLAYBADNAMEERRORMESSAGE:
                                                                                   userPokemon.ToString()));
    while (true){
        Console.WriteLine("Want to search again? Enter Y or N");
        userInput = Console.ReadLine();
        Console.Clear();
        if ("y".Equals(userInput, StringComparison.CurrentCultureIgnoreCase))
        {
            break;
        }
        else if ("n".Equals(userInput, StringComparison.CurrentCultureIgnoreCase)){
           Quit();
        }
    }
    
}
