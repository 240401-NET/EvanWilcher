﻿using System;
using System.Collections.Generic;

namespace ASP_Project.Model;

public partial class PokemonAbility
{
    public int Id { get; set; }

    public bool IsHidden { get; set; }

    public int Slot { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int PokemonId { get; set; }

    public virtual Pokemon Pokemon { get; set; } = null!;
}
