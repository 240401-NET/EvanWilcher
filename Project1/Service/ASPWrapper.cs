using System.Text.Json;
using System.Text.Json.Nodes;

public static class ASPWrapper{
    static HttpClient client;
    static string data = "";

    private static async void GetCallout(string? _pokemon){
        client = new()
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/")
        };
        var response = client.GetAsync(_pokemon).Result;
        if(response.IsSuccessStatusCode){
            Console.WriteLine("Searching...");
            data = await response.Content.ReadAsStringAsync();
        }
    }

    public static Pokemon? GetPokemonData(string? _pokemon){
            if (_pokemon == null)
                return null;
            GetCallout(_pokemon);
            // for debugging
            if(data == null || data == "")
                return null;

            JsonNode? pokemonNode = JsonNode.Parse(data);
            Console.Clear();
            return new Pokemon(pokemonNode["id"]!.GetValue<int>(), pokemonNode["name"]!.GetValue<string>(), pokemonNode["base_experience"]!.GetValue<int>(), 
                               pokemonNode["height"]!.GetValue<int>(), pokemonNode["is_default"]!.GetValue<bool>(), pokemonNode["order"]!.GetValue<int>(), 
                               pokemonNode["weight"]!.GetValue<int>());
    }
}