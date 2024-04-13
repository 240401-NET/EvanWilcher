namespace ASP_Project;

public partial class PokemonMove
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int PokemonId { get; set; }

    public virtual Pokemon Pokemon { get; set; } = null!;
}
