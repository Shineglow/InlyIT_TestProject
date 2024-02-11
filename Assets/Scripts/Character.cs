using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    public string Id { get; private set; }
    public string ObjectName { get; private set; }
    public string Description { get; private set; }
    public Sprite Icon { get; private set; }
    public Transform Mesh { get; private set; }
    public ICharacterProperties Properties { get; private set; }

    private MeshRenderer meshFilter;

    public void SetCharacter(ICharacterProperties properties)
    {
        Properties = properties;
        Id = properties.Id;
        ObjectName = properties.Name;
        Description = properties.Description;
        
        Icon = ResourcesManager.LoadResource<Sprite, ECharacterIcons>(properties.Icon);
        Mesh = ResourcesManager.LoadResource<GameObject, ECharacterModel>(properties.Model).transform;
        meshFilter = Mesh.GetComponent<MeshRenderer>();
        meshFilter.material = ResourcesManager.LoadResource<Material, ECharacterMaterial>(properties.Skin);
    }
}

public interface ICharacter
{
    string Id {get;}
    string ObjectName {get;}
    string Description {get;}
    Sprite Icon {get;}
    Transform Mesh { get; }
    
    ICharacterProperties Properties { get; }

    void SetCharacter(ICharacterProperties properties);
}
