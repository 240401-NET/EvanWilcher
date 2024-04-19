using ASP_Project.Model;
using ASP_Project.Repository;
using System.Text.Json.Nodes;

namespace ASP_Project.Service;
/// REST up! we're making API calls!
public class RESTPKMService : IRESTPKMService
{
    private readonly HttpClient client;

    public RESTPKMService(HttpClient _client) => client = _client;

    // our GET function.
    // one all model objects are created may create more
    // GET functions for different callouts
    private string GetPokemonCallout(string? _pokemonEndpoint){
        string pokemonData = "";
        // new client!
        client.BaseAddress = new Uri(Globals.POKEMONAPI_POKEMON);

        // GET IT GET IT GET IT GET IT!
        var response = client.GetAsync(_pokemonEndpoint).Result;
        // ...did we get it?
        if(response.IsSuccessStatusCode){
            // oh yeah we got that!
            // set data and let's move on
            pokemonData = response.Content.ReadAsStringAsync().Result;
        }
        return pokemonData;
    }
    private string GetItemCallout(string? _itemEndpoint){
        string itemData = "";
        client.BaseAddress = new Uri(Globals.POKEMONAPI_ITEMS);
        var response = client.GetAsync(_itemEndpoint).Result;
        if (response.IsSuccessStatusCode)
            itemData = response.Content.ReadAsStringAsync().Result;
        return itemData;
    }
    // Wrapper function that calls GetCallout and returns a pokemon
    public Pokemon? GetPokemonData(string? _pokemonName){
        // null check
        if (_pokemonName == null)
            return null;
        // the call
        string tempString = GetPokemonCallout(_pokemonName);
        string pokemonData = tempString;
        Console.WriteLine(pokemonData);
        // null check
        if(pokemonData == null || pokemonData == "")
            return null;
        // let's get parsing!!!
        // we're doing it this way because the models are not fully ready
        JsonNode? pokemonNode = JsonNode.Parse(pokemonData);
        if (pokemonNode == null)
            return null;
        // return user's pokemans!
        Pokemon tempPokemon = new(){
            Name = pokemonNode["name"]!.GetValue<string>(),
            Weight = pokemonNode["weight"]!.GetValue<int>(),
            ChosenAbility = "",
            ChosenTeraType = "Normal",
            PokemonSprite = new(){
                FrontDefault = pokemonNode["sprites"]!["front_default"]!.GetValue<string>(),
                FrontShiny = pokemonNode["sprites"]!["front_shiny"]!.GetValue<string>(),
                FrontFemale = pokemonNode["sprites"]!["front_female"]! == null ? pokemonNode["sprites"]!["front_default"]!.GetValue<string>() : pokemonNode["sprites"]!["front_female"]!.GetValue<string>(),
                FrontShinyFemale = pokemonNode["sprites"]!["front_shiny_female"]! == null ? pokemonNode["sprites"]!["front_shiny"]!.GetValue<string>() : pokemonNode["sprites"]!["front_shiny_female"]!.GetValue<string>()
                }
        };
        var tempList = pokemonNode["abilities"]!.AsArray().ToList();
        if (tempList.Any())
        {
            foreach (JsonNode? jn in tempList){
                if (jn == null)
                    continue;
                tempPokemon.PokemonAbilities.Add(new(){
                    Name = jn["ability"]!["name"]!.GetValue<string>(), 
                    Url = jn["ability"]!["url"]!.GetValue<string>(),
                    IsHidden = jn["is_hidden"]!.GetValue<bool>(),
                    Slot = jn["slot"]!.GetValue<int>()
                });
            } 
        }
        tempList = pokemonNode["moves"]!.AsArray().ToList();
        if(tempList.Any()){
            foreach(JsonNode? jn in tempList){
                if (jn == null)
                    continue;
                tempPokemon.PokemonMoves.Add(new(){
                    Name = jn["move"]!["name"]!.GetValue<string>(),
                    Url = jn["move"]!["url"]!.GetValue<string>()
                });
            }
        }
        // tempList = pokemonNode["stats"]!.AsArray().ToList();
        // if (tempList.Any()){
        //         foreach(JsonNode? jn in tempList){
        //             if (jn == null)
        //                 continue;
        //             tempPokemon.PokemonStats.Add(new(){
        //                 Name = jn["stat"]!["name"]!.GetValue<string>(),
        //                 Url = jn["stat"]!["url"]!.GetValue<string>(),
        //                 BaseStat = jn["base_stat"]!.GetValue<int>()
        //             });
        //         }
        //     }
        tempList = pokemonNode["types"]!.AsArray().ToList();
        if (tempList.Any()){
                foreach(JsonNode? jn in tempList){
                    if (jn == null)
                        continue;
                    tempPokemon.PokemonTypes.Add(new(){
                        Slot = jn["slot"]!.GetValue<int>(),
                        Name = jn["type"]!["name"]!.GetValue<string>(),
                        Url = jn["type"]!["url"]!.GetValue<string>()
                    });
                }
            }
        return tempPokemon;
    }
     
    public PokemonHeldItem? GetItem(string? _itemName){
         if (_itemName == null)
            return null;
        string tempString = GetItemCallout(_itemName);
        string itemData = tempString;
        if(itemData == null || itemData == "")
            return null;
        JsonNode? itemNode = JsonNode.Parse(itemData);
        if (itemNode == null)
            return null;
        PokemonHeldItem tempPKMItem = new(){
            Name = itemNode["name"]!.GetValue<string>(),
            Sprite = itemNode["sprites"]!["default"]!.GetValue<string>()
        };
        return tempPKMItem; 
    }
}