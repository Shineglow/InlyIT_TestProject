using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IdGenerator
{
    private static int lastID = int.MinValue;

    public static int GetNextNumericID() => lastID++;
    public static string GetNextStringID() => GetNextNumericID().ToString();
}
