namespace Keeper.Domain;

public class Profile: Entity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Position { get; set; }
    public string Image { get; set; }
}
