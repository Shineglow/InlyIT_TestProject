public class CharacterProperties : ICharacterProperties
{
    public string Id { get; set; } = IdGenerator.GetNextStringID();
    public string Name { get; set; }
    public string Description { get; set; }
    public ECharacterIcons Icon { get; set; }
    public ECharacterModel Model { get; set; }
    public ECharacterMaterial Skin { get; set; }
}

public interface ICharacterProperties
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public ECharacterIcons Icon { get; }
    public ECharacterModel Model { get; }
    public ECharacterMaterial Skin { get; }
}
