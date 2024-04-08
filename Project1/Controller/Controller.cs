public class Controller{
    // Constants
    protected string? userInput;  //buffer for user input
    public Trainer localTrainer = new();
    protected List<Trainer.PokemonTeam> localTeams = new();
    protected IControllerToMenu menuInterface;
    public Controller(IControllerToMenu _menuInterface){
        menuInterface = _menuInterface;
    }

    // quit the console application
    public void Quit(){
        Console.Clear();
        Console.WriteLine("Goodbye Trainer!");
        Environment.Exit(0);
    }
    public void Intro(){
        Console.Clear();
        Console.WriteLine("\\o  Hello Trainer!  o/");
        FileService.LoadData();
        CheckPersistUser();
    }
    private void CheckPersistUser(){
        while (true){
            Console.WriteLine("What is your name?");
            userInput = Console.ReadLine();
            Console.Clear();
            if (userInput == null || userInput == ""){
                Console.Clear();
                Console.WriteLine("Please enter a name.");
                continue;
            }
            if (FileService.DoesTrainerPresist(userInput.TrimEnd())){
                SetLocalTrainer(userInput);
                Console.WriteLine($"Welcome back {userInput}!!!");
                menuInterface.UpdateMenuState(Menu.EMenuState.CURR_TEAM);
                return;
               }else{
                Console.WriteLine($"Welcome {userInput}, since you're new let's get some pokemon\nfor your first team using PokeAPI!");
                localTrainer = new();
                localTrainer.name = userInput;
                localTrainer.team = new();
                menuInterface.UpdateMenuState(Menu.EMenuState.SEARCH);
                return;
            }
        }
    }
    public void SearchPokemon(){
        Console.Clear();
        while (true){
            // heads up user!
            Console.WriteLine("What pokemon would you like to search?\nOr, enter 'q' to quit.");
            userInput = Console.ReadLine();

            // q for quit.
            if("q".Equals(userInput.ToLower(), StringComparison.CurrentCultureIgnoreCase)){
                menuInterface.UpdateMenuState(Menu.EMenuState.QUIT);
                return;
            }

            // clear the screen
            // TODO::Wrapper function to presist information on the screen
            //       after clearing the screen
            Console.Clear();

            // GOTTA GET'EM ALL! (or just the one the user asked for)
            Pokemon? userPokemon = RESTService.GetPokemonData(userInput.ToLower());
            Console.WriteLine("****************************************");
            //Error handling and a good candidate for try/catch block 
            Console.WriteLine((userInput == null|| userInput =="") ? Globals.DISPLAYBADNAMEERRORMESSAGE : 
                                                                    (userPokemon == null ? Globals.DISPLAYBADNAMEERRORMESSAGE:
                                                                                           userPokemon.ToString()));
            Console.WriteLine("****************************************");
            
            if (!ContinueSeraching(userPokemon))
                return;
        }
    }
    public void DisplayTeams(){
        Console.Clear();
        while (true){
            Console.WriteLine($"Here are your teams.");
            Console.WriteLine("****************************************");
            int i = 1;
            foreach (Trainer.PokemonTeam pkmTeam in localTeams){
                Console.Write($"{i++}.");
                foreach (Pokemon pkm in pkmTeam.pokemonTeam)
                    Console.Write($"{pkm}\t\t");
                Console.Write("\n\n");
            }          
            Console.WriteLine("****************************************");
            Console.Write("\n\n");
            Console.WriteLine("Press \"S\" to search for a Pokemon for your team." +
                              "\nPress team number to select team. " +
                              "\nPress \"C\" to see your current team." + 
                              "\nPress \"Q\" to quit. ");
            userInput = Console.ReadLine();
            if ("q".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                menuInterface.UpdateMenuState(Menu.EMenuState.QUIT);
                return;
            }
            else if ("s".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                menuInterface.UpdateMenuState(Menu.EMenuState.SEARCH);
                return;
            }
            else if ("c".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){

                return;
            }
            else if (Convert.ToInt32(userInput) >=1 && 
                     Convert.ToInt32(userInput) <= localTeams.Count)
                    {
                        // todo::display a team and make it the current team for
                        // the user (localTrainer.team = chosen team)
                        if(AffirmTeam()){
                            localTrainer = FileService.GetTrainer(localTrainer.name);
                            FileService.SaveData(localTrainer);
                            menuInterface.UpdateMenuState(Menu.EMenuState.CURR_TEAM);
                        }
                            Console.Clear();
                        return;

                    }  
        }            
    }
    public void DisplayCurrTeam(){
        Console.Clear();
        while(true){
            Console.WriteLine("Here is your current team.");
            Console.WriteLine("****************************************");
            foreach (Pokemon pkm in localTrainer.PKMTeam)
                    Console.WriteLine($"{pkm}\n");
            Console.WriteLine("****************************************");
            Console.WriteLine("Press \"S\" to search for a Pokemon for your team." +
                            //   "\nPress \"T\" to see all of your teams. " +
                              "\nPress \"Q\" to quit. ");
            userInput = Console.ReadLine();
            if ("q".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                menuInterface.UpdateMenuState(Menu.EMenuState.QUIT);
                return;
            }
            else if ("s".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                menuInterface.UpdateMenuState(Menu.EMenuState.SEARCH);
                return;
            }
            // else if ("t".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
            //     menuInterface.UpdateMenuState(Menu.EMenuState.TEAMS);
            //     return;
            // }
        }    
    }
    private void SaveTeam(){
        Console.Clear();
        if(localTrainer.team.name == string.Empty){
            Console.WriteLine("Name your team!");
            userInput = Console.ReadLine();
            Trainer.PokemonTeam tempTeam = localTrainer.team;
            tempTeam.name = userInput;
            localTrainer.team = tempTeam;
            Console.WriteLine("Great name!");
            FileService.SaveData(localTrainer);
        }
        else{
            while (true){
                Console.WriteLine($"Overwrite team {localTrainer.team.name} Y/N?");
                userInput = Console.ReadLine();
                if ("y".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                    Console.WriteLine($"Overwriting team {localTrainer.team.name}.");
                    FileService.SaveData(localTrainer);
                    break;
                }  
                else if ("n".Equals(userInput.ToLower(), StringComparison.CurrentCulture))
                {
                    while (true){
                        Console.WriteLine("Rename team Y/N?");
                        userInput = Console.ReadLine();
                        if ("y".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                            Trainer.PokemonTeam tempTeam = localTrainer.team;
                            tempTeam.name = string.Empty;
                            localTrainer.team = tempTeam;
                            SaveTeam();
                            return;
                        }
                        else if ("n".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                            Console.WriteLine("Will not save team.");
                            return;
                        }
                        Console.Clear();
                        Console.WriteLine("Incorrect choice.");
                    }
                    
                }
                Console.Clear();
                Console.WriteLine("Incorrect choice.");
            }
            
        }
    }
    protected void SetLocalTrainer(string? _trainerName){
        localTrainer = FileService.GetTrainer(_trainerName);  
        //localTeams = FileService.GetTrainersTeamList(localTrainer.name);
    }
    protected bool ContinueSeraching(Pokemon _searchedPokmon){
        if (_searchedPokmon == null){
            Console.WriteLine("No Pokemon to add to team.");
            while (true){
                // heads up user!
            
                Console.WriteLine("Press \"S\" to search again." +
                                //   "\nPress \"T\" to see all of your teams." +
                                  "\nPress \"C\" to see your current team." +
                                  "\nPress \"Q\" to quit. ");
                userInput = Console.ReadLine();

                // clear the screen.
                    Console.Clear();
                if ("s".Equals(userInput.ToLower(), StringComparison.CurrentCulture))
                    return true;

                else if ("q".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                    menuInterface.UpdateMenuState(Menu.EMenuState.QUIT);
                    return false;
                }
                else if ("c".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                    menuInterface.UpdateMenuState(Menu.EMenuState.CURR_TEAM);
                    return false;
                }
                // else if ("t".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                //     menuInterface.UpdateMenuState(Menu.EMenuState.TEAMS);
                //     return false;
                // }

                Console.Clear();
                Console.WriteLine("Incorrect input.");    
            }
        }
        while (true){
            // heads up user!
            
            Console.WriteLine("Press \"A\" to add to your party." +
                              "\nPress \"S\" to search again." +
                            //   "\nPress \"T\" to see all of your teams." +
                              "\nPress \"C\" to see your current team." +
                              "\nPress \"Q\" to quit. ");
            userInput = Console.ReadLine();

            // clear the screen.
            Console.Clear();

            if ("a".Equals(userInput.ToLower(), StringComparison.CurrentCulture))
                return AddPokemonToTeam(_searchedPokmon);
            else if ("s".Equals(userInput.ToLower(), StringComparison.CurrentCulture))
                return true;
            else if ("q".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                menuInterface.UpdateMenuState(Menu.EMenuState.QUIT);
                return false;
            }
            else if ("c".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                menuInterface.UpdateMenuState(Menu.EMenuState.CURR_TEAM);
                return false;
            }
            // else if ("t".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
            //     menuInterface.UpdateMenuState(Menu.EMenuState.TEAMS);
            //     return false;
            // }
            
            Console.Clear();
            Console.WriteLine("Incorrect input.");
        }
    }
    protected bool AddPokemonToTeam(Pokemon _searchedPokmon){
        Console.Clear();
        List<string> PKMNames = new();
        if(!localTrainer.PKMTeam.Any() || 
           localTrainer.PKMTeam.Count < Globals.MAXPKMTEAMSIZE){
            Console.WriteLine($"Adding {_searchedPokmon.Name} to your current team.");
            localTrainer.PKMTeam.Add(_searchedPokmon);
            SaveTeam();
        }
        else if (localTrainer.PKMTeam.Count == Globals.MAXPKMTEAMSIZE){
            while (true){
                Console.WriteLine("Your team is full. Enter Pokemon name or team position to" +
                                  "to trade out Pokemon, or press \"B\" to go back to searching.");
                for(int j = 0; j < Globals.MAXPKMTEAMSIZE; j++){
                    Console.WriteLine($"{j}) {localTrainer.PKMTeam[j].Name}");
                    PKMNames.Add(localTrainer.PKMTeam[j].Name);
                }
                userInput = Console.ReadLine();
                if ("b".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                    Console.WriteLine("Going back to searching.");
                    return true;
                }
                else if (Convert.ToInt32(userInput) >=1 && 
                         Convert.ToInt32(userInput) <= Globals.MAXPKMTEAMSIZE){
                    int index = Convert.ToInt32(userInput);
                    Console.WriteLine($"Exchanging {_searchedPokmon.Name} for {localTrainer.PKMTeam[index].Name}!");
                    localTrainer.PKMTeam[index] = _searchedPokmon;
                    SaveTeam();
                    break;
                }
                else if (PKMNames.Contains(userInput)){
                    for(int j = 0; j < Globals.MAXPKMTEAMSIZE; j++){
                        if (userInput == localTrainer.PKMTeam[j].Name){
                            Console.WriteLine($"Exchanging {_searchedPokmon.Name} for {localTrainer.PKMTeam[j].Name}!");
                            localTrainer.PKMTeam[j] = _searchedPokmon;
                            SaveTeam();
                            break;
                        }
                    }
                    break;
                }

                Console.Clear();
                Console.WriteLine("Incorrect input.");
            }            
        }
        while (true){
                // heads up user!
                Console.WriteLine("Press \"S\" to search again." +
                                //   "\nPress \"T\" to see all of your teams." +
                                  "\nPress \"C\" to see your current team." +
                                  "\nPress \"Q\" to quit. ");
                userInput = Console.ReadLine();

                // clear the screen.
                    Console.Clear();
                if ("s".Equals(userInput.ToLower(), StringComparison.CurrentCulture))
                    return true;

                else if ("q".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                    menuInterface.UpdateMenuState(Menu.EMenuState.QUIT);
                    return false;
                }
                else if ("c".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                    menuInterface.UpdateMenuState(Menu.EMenuState.CURR_TEAM);
                    return false;
                }
                // else if ("t".Equals(userInput.ToLower(), StringComparison.CurrentCulture)){
                //     menuInterface.UpdateMenuState(Menu.EMenuState.TEAMS);
                //     return false;
                // }

                Console.Clear();
                Console.WriteLine("Incorrect input.");    
            }
            
    }
    protected bool AffirmTeam(){
        Console.Clear();
            while (true){
                Console.WriteLine("Go with this team, Y/N?");
                userInput = Console.ReadLine();
                if ("y".Equals(userInput.ToLower()))
                    return true;
                else if ("n".Equals(userInput.ToLower()))
                    return false;
                Console.Clear();
                Console.WriteLine("Please use Y/N for input.");
            }
    } 
   }