using System;
using System.Collections.Generic;

namespace ASP_Project.Model;

public partial class PokemonHeldItem
{
    public string Name { get; set; } = null!;

    public int PokemonId { get; set; }

    public string Sprite { get; set; } = null!;

    public virtual Pokemon Pokemon { get; set; } = null!;
}
