using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeviceInput
{
    bool AnyButtonPressed { get; }

    float GetIndexedButton(int index);
    Vector2 GetAxis(int index);
}
