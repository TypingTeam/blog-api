namespace Keeper.Domain;

public class Profile: Entity
{
    private Profile() { }
    
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Position { get; private set; }
    public string Image { get; private set; }
    public bool IsActive { get; private set; }

    public static Profile Create(string name, string surname, string position, string image)
    {
        return new Profile { Name = name, Surname = surname, Position = position, Image = image, IsActive = true};
    }

    public bool Deactivate()
    {
        IsActive = false;
        return IsActive;
    }
}
