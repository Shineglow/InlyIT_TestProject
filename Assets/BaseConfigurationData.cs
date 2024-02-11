using System.Collections.Generic;
using UnityEngine;

public interface IBaseConfigurationData
{
    public string Id {get;}
    public string ObjectName {get;}
    public string Description {get;}
    public Sprite Icon {get;}
}

public interface IMeshProperty
{
    public Mesh Mesh { get; }
}

public interface ICharacterConfigurationData : IBaseConfigurationData, IMeshProperty{}

public class CharacterConfigurationData : ICharacterConfigurationData
{
    public string Id { get; set; }
    public string ObjectName { get; set; }
    public string Description { get; set; }
    public Sprite Icon { get; set; }
    public Mesh Mesh { get; set; }
}