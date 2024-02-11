using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasLayers : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Canvas canvas;
    
    [SerializeField] private RectTransform layer1;
    [SerializeField] private RectTransform layer2;
    [SerializeField] private RectTransform layer3;

    public RectTransform Layer1 => layer1;
    public RectTransform Layer2 => layer2;
    public RectTransform Layer3 => layer3;

    private void Start()
    {
        canvas.worldCamera = Camera.current;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        
        
    }
}
