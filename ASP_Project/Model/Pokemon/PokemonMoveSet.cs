namespace ASP_Project;

public partial class PokemonMoveSet
{
    public string Move1 { get; set; } = null!;

    public string Move2 { get; set; } = null!;

    public string Move3 { get; set; } = null!;

    public string Move { get; set; } = null!;

    public int PokemonId { get; set; }

    public virtual Pokemon Pokemon { get; set; } = null!;
}
