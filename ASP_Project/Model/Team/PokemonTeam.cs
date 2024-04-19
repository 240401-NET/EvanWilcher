using System;
using System.Collections.Generic;

namespace ASP_Project.Model;

public partial class PokemonTeam
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TrainerId { get; set; }

    public virtual ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();

    public virtual Trainer Trainer { get; set; } = null!;
}
