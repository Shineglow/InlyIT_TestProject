using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardMouseInput : MonoBehaviour, IDeviceInput
{
    public bool AnyButtonPressed { get; private set; }
    
    
    private List<float> buttons = new();
    public float GetIndexedButton(int index)
    {
        return buttons.ElementAtOrDefault(index);
    }

    private List<Vector2> axes = new();
    public Vector2 GetAxis(int index)
    {
        return axes.ElementAtOrDefault(index);
    }
    
    private Dictionary<EKeyboardMouseAxes, (string horizontal, string vertical)> axesNamesMap;
    
    private void Awake()
    {
        foreach (var _ in Enum.GetValues(typeof(EKeyboardMouseButtons)))
            buttons.Add(0);
        
        foreach (var _ in Enum.GetValues(typeof(EKeyboardMouseAxes)))
            axes.Add(Vector2.zero);

        axesNamesMap = new()
        {
            {EKeyboardMouseAxes.WASD, ("Horizontal", "Vertical")},
            {EKeyboardMouseAxes.MouseDelta, ("Mouse X", "Mouse Y")},
            {EKeyboardMouseAxes.Arrows, ("Horizontal3", "Vertical3")},
            {EKeyboardMouseAxes.MinusPlus, ("Alt1", "Alt2")},
        };
    }
    private void Update()
    {
        foreach (KeyCode en in Enum.GetValues(typeof(KeyCode)))
            if(Input.GetKey(en)) Debug.Log(en);
        
        // foreach (KeyCode keyCode in Enum.GetValues(typeof(EKeyboardMouseButtons)))
        // {
        //     var keyPressed = Input.GetKey(keyCode);
        //     buttons[(int)keyCode] = keyPressed ? 1f : 0f;
        //     AnyButtonPressed |= keyPressed;
        // }
        //
        // foreach (EKeyboardMouseAxes val in Enum.GetValues(typeof(EKeyboardMouseAxes)))
        // {
        //     var value = new Vector2();
        //     var axisNames = axesNamesMap[val];
        //     value.x = Input.GetAxis(axisNames.horizontal);
        //     value.y = Input.GetAxis(axisNames.vertical);
        //     axes[(int)val] = value;
        // }
    }

    protected enum EKeyboardMouseButtons
    {
        Space = KeyCode.Space,
        Shift = KeyCode.LeftShift,
        RMB = KeyCode.Mouse1,
        LMB = KeyCode.Mouse0,
        Q = KeyCode.Q,
        E = KeyCode.E,
        MMB = KeyCode.Mouse2,
        M = KeyCode.M,
        Esc = KeyCode.Escape,
        Enter = KeyCode.Return,
    }
    
    protected enum EKeyboardMouseAxes
    {
        WASD,
        MouseDelta,
        Arrows,
        MinusPlus,
    }
}
