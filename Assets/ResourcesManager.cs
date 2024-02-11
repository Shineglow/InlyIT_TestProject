using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ResourcesManager
{
    public static T LoadResource<T, E>(E enumOfResource) where T : Object where E : Enum
    {
        var path = $"{typeof(E).Name}/{enumOfResource.ToString()}";
        var result = Resources.Load<T>(path);

        return result;
    }
    
    public static T CreatePrefabInstance<T, E>(E item, Transform parent = null) where E : Enum
    {
        var path = $"{typeof(E).Name}/{item.ToString()}";
        var asset = Resources.Load<GameObject>(path);
        var prefab = parent == null ? Object.Instantiate(asset) : Object.Instantiate(asset, parent);
        var result = prefab.GetComponent<T>();

        return result;
    }
}
