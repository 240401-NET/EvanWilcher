using System;
using System.Collections.Generic;

namespace ASP_Project.Model;

public partial class PokemonType
{
    public int Id { get; set; }

    public int Slot { get; set; }

    public string Url { get; set; } = null!;

    public int PokemonId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Pokemon Pokemon { get; set; } = null!;
}
