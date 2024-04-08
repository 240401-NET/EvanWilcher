
using System.Security.Cryptography.X509Certificates;

public interface IControllerToMenu{
        void UpdateMenuState(Menu.EMenuState _state);
}
public class Menu : IControllerToMenu{
     public enum EMenuState{
        START,
        SEARCH,
        TEAMS,
        CURR_TEAM,
        QUIT
    }
    EMenuState state = EMenuState.START;
    Controller controller;

    public Menu(){
        controller = new(this);
    }

    public void Run(){
        bool isRunning = true;
        while (isRunning)
        {
            switch(state){
                case EMenuState.START:{
                    controller.Intro();
                    break;
                }
                case EMenuState.SEARCH:{
                    controller.SearchPokemon();
                    break;
                }
                case EMenuState.TEAMS:{
                    controller.DisplayTeams();
                    break;
                }
                case EMenuState.CURR_TEAM:{
                    controller.DisplayCurrTeam();
                    break;
                }
                case EMenuState.QUIT:{
                    controller.Quit();
                    break;
                }
            }
        }
    }

    public void UpdateMenuState(Menu.EMenuState _state)
    {
        state = _state;
    }
}