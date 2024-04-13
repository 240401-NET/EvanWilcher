[Serializable]
public class Trainer{
    public struct PokemonTeam{
        public string name {get; set;} = string.Empty;
        public List<Pokemon> pokemonTeam {get; set;} = new List<Pokemon>(Globals.MAXPKMTEAMSIZE);
        public PokemonTeam(){}
        public override string ToString(){
            string returnString = "";
            foreach (Pokemon pokemon in pokemonTeam)
                if (pokemon != null)
                    returnString += pokemon.ToString() + ", ";
            
            return returnString; 
        }
    };
   
    /* id can't be private since it's static and i'm using
       field properties.  set will need to have an access
       modifier higher than the field. */
    // static public int? id{
    //     get { return id; }
    //     private set { id = value; }
    // }

    public string name {get; set;} = string.Empty;
    public PokemonTeam team {get; set;}
    public List<Pokemon> PKMTeam{
        get{return team.pokemonTeam;}
        set{}
    }
    public bool ExchangePokemon(Pokemon _pkm, int _index) 
    {
        if (_index >= 0 &&
            _index < 6){
            PKMTeam[_index]= _pkm;
            return true;    
        }
        Console.WriteLine("Error. Index is out of bounds.");
        return false;        
    }
}