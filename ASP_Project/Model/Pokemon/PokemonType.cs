namespace ASP_Project;

public partial class PokemonType
{
    public int Id { get; set; }

    public int Slot { get; set; }

    public int Type { get; set; }

    public int PokemonId { get; set; }

    public virtual Pokemon Pokemon { get; set; } = null!;
}
