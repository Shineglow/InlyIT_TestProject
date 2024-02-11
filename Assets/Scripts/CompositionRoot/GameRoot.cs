using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot
{
    private static Configurations configurations;
    public static Configurations Configurations => configurations ??= new Configurations();

    private static CanvasLayers canvasLayers;
    public static CanvasLayers CanvasLayers => canvasLayers ??= ResourcesManager.CreatePrefabInstance<CanvasLayers, EViews>(EViews.UIHierarchy);
}
