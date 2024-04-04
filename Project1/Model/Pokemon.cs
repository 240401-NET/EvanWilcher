using System.Security.Cryptography.X509Certificates;
// Pokeman's object.  To be continued.
public class Pokemon(int? id, 
                     string? name, 
                     int? baseExperience, 
                     int? height, 
                     bool? isDefault, 
                     int? order, 
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
}