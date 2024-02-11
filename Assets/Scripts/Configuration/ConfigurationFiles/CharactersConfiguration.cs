
public class CharactersConfiguration : BaseConfiguration<ICharacterProperties, ECharacters>
{
    public CharactersConfiguration()
    {
        InitializationOfCharactersProperties();
    }

    private void InitializationOfCharactersProperties()
    {
        data = new()
        {
            { ECharacters.Blue , new CharacterProperties()
            {
                Name = "Blue",
                Description = "He's as blue as the sky",
                Icon = ECharacterIcons.BlueCharacter,
                Model = ECharacterModel.Cube,
                Skin = ECharacterMaterial.BlueSkin
            }},
            { ECharacters.Red , new CharacterProperties()
            {
                Name = "Red",
                Description = "He's red like wine",
                Icon = ECharacterIcons.RedCharacter,
                Model = ECharacterModel.Sphere,
                Skin = ECharacterMaterial.RedSkin
            }},
            { ECharacters.Yellow , new CharacterProperties()
            {
                Name = "Yellow",
                Description = "She's yellow like the sun",
                Icon = ECharacterIcons.YellowCharacter,
                Model = ECharacterModel.Cube,
                Skin = ECharacterMaterial.YellowSkin
            }},
            { ECharacters.White , new CharacterProperties()
            {
                Name = "White",
                Description = "She's as white as snow",
                Icon = ECharacterIcons.WhiteCharacter,
                Model = ECharacterModel.Sphere,
                Skin = ECharacterMaterial.WhiteSkin
            }},
        };
    }
}


public enum ECharacters
{
    Red,
    Blue,
    Yellow,
    White,
}
