using System;
using System.Collections.Generic;

namespace ASP_Project;

public partial class PokemonStat
{
    public int Effort { get; set; }

    public int Individual { get; set; }

    public int BaseStat { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int PokemonId { get; set; }

    public int Total { get; set; }

    public int Id { get; set; }

    public virtual Pokemon Pokemon { get; set; } = null!;
}
