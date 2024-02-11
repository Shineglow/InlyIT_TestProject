using UnityEngine;

public interface IGameObjectBaseProperties
{
    public string Id {get;}
    public string ObjectName {get;}
    public string Description {get;}
    public Sprite Icon {get;}
    public Mesh Mesh { get; }
}
