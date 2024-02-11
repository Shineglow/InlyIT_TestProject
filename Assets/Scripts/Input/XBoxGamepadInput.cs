using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class XBoxGamepadInput : MonoBehaviour, IDeviceInput
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


    private Dictionary<EGamepadAxes, (string horizontal, string vertical)> axesNamesMap;

    private void Awake()
    {
        foreach (var _ in Enum.GetValues(typeof(EGamepadButtons)))
            buttons.Add(0);
        
        foreach (var _ in Enum.GetValues(typeof(EGamepadAxes)))
            axes.Add(Vector2.zero);

        axesNamesMap = new()
        {
            {EGamepadAxes.LeftStick, ("Horizontal", "Vertical")},
            {EGamepadAxes.RightStick, ("Mouse X", "Mouse Y")},
            {EGamepadAxes.DPad, ("Horizontal3", "Vertical3")},
            {EGamepadAxes.LTRT, ("Fire1", "Fire2")},
        };
    }
    private void Update()
    {
        foreach (int button in Enum.GetValues(typeof(EGamepadButtons)))
        {
            var buttonName = "joystick button " + button;
            var keyPressed = Input.GetKey(buttonName);
            buttons[button] = keyPressed ? 1f : 0f;
            AnyButtonPressed |= keyPressed;
        }

        foreach (EGamepadAxes val in Enum.GetValues(typeof(EGamepadAxes)))
        {
            var value = new Vector2();
            var axisNames = axesNamesMap[val];
            value.x = Input.GetAxis(axisNames.horizontal);
            value.y = Input.GetAxis(axisNames.vertical);
            axes[(int)val] = value;
        }
    }
    
    protected enum EGamepadButtons
    {
        A,
        B,
        X,
        Y,
        LB,
        RB,
        LS,
        RS,
        Back,
        Start,
    }

    protected enum EGamepadAxes
    {
        LeftStick,
        RightStick,
        DPad,
        LTRT,
    }
}
