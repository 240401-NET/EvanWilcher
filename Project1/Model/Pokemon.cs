using System.Security.Cryptography.X509Certificates;
using Microsoft.Net.Http.Headers;
// Pokeman's object.  To be continued.
[Serializable]
public class Pokemon(int id, 
                     string name, 
                     int baseExperience, 
                     int height, 
                     bool isDefault, 
                     int order, 
                     int weight
                     /*List<string>? abilities, 
                     List<string>? forms,
                     List<string>? gameIndicies, 
                     List<string>? heldItems, 
                     List<string>? locationAreaEncounters, 
                     List<string>? moves,
                     List<string>? species, 
                     List<string>? sprites, 
                     List<string>? stats, 
                     List<string>? types,
                     List<string>? pastTypes*/){
    public override string ToString(){
        return $"Name: {name}\nHeight: {height}\nWeight: {weight}"; 
    }
    public int ID {get; private set;} = id;
    public string Name {get; private set;} = name;
    public int BaseExperience {get; private set;} = baseExperience;
    public int Height {get; private set;} = height;
    public bool IsDefault {get; private set;} = isDefault;
    public int Order {get; private set;} = order;
    public int Weight {get; private set;} = weight;
}