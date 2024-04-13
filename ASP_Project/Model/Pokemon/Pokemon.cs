using System;
using System.Collections.Generic;

namespace ASP_Project;

public partial class Pokemon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Weight { get; set; }

    public int TeamId { get; set; }

    public string ChosenAbility { get; set; } = null!;

    public bool IsShiny { get; set; }

    public byte Level { get; set; }

    public string ChosenTeraType { get; set; } = null!;

    public virtual ICollection<PokemonAbility> PokemonAbilities { get; set; } = new List<PokemonAbility>();

    public virtual PokemonHeldItem? PokemonHeldItem { get; set; }

    public virtual PokemonMoveSet? PokemonMoveSet { get; set; }

    public virtual ICollection<PokemonMove> PokemonMoves { get; set; } = new List<PokemonMove>();

    public virtual ICollection<PokemonStat> PokemonStats { get; set; } = new List<PokemonStat>();

    public virtual ICollection<PokemonType> PokemonTypes { get; set; } = new List<PokemonType>();

    public virtual PokemonTeam Team { get; set; } = null!;
}
