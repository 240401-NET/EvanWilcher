using System;
using System.Collections.Generic;

namespace ASP_Project;

public partial class Trainer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PokemonTeam> PokemonTeams { get; set; } = new List<PokemonTeam>();
}
