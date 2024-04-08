using System.Text.Json;
using System.Text.Json.Nodes;

/// REST up! we're making API calls!
public static class RESTService{
    // client is what we're going to use to connect
    // to the endpoint.
    static HttpClient client;
    // putting this here cause scope was beatting me up.
    static string data = "";

    // our GET function.
    // one all model objects are created may create more
    // GET functions for different callouts
    private static async void GetCallout(string? _pokemon){
        // new client!
        client = new()
        {
            // set the endpoint!
            BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/")
        };
        Console.WriteLine("Searching...");
        // GET IT GET IT GET IT GET IT!
        var response = client.GetAsync(_pokemon).Result;
        // ...did we get it?
        if(response.IsSuccessStatusCode){
            // oh yeah we got that!
            // set data and let's move on
            data = await response.Content.ReadAsStringAsync();
        }
    }

    // Wrapper function that calls GetCallout and returns a pokemon
    public static Pokemon? GetPokemonData(string? _pokemon){
            // null check
            if (_pokemon == null)
                return null;
            // the call
            GetCallout(_pokemon);
            // null check
            if(data == null || data == "")
                return null;
            // let's get parsing!!!
            // we're doing it this way because the models are not fully ready
            JsonNode? pokemonNode = JsonNode.Parse(data);
            // clear the window
            Console.Clear();
            // return user's pokemans!
            return new Pokemon(pokemonNode["id"]!.GetValue<int>(), pokemonNode["name"]!.GetValue<string>(), pokemonNode["base_experience"]!.GetValue<int>(), 
                               pokemonNode["height"]!.GetValue<int>(), pokemonNode["is_default"]!.GetValue<bool>(), pokemonNode["order"]!.GetValue<int>(), 
                               pokemonNode["weight"]!.GetValue<int>());
    }
}