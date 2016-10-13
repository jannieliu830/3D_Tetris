// Upgrade NOTE: replaced 'Material' with 'UnityEngine.Material'

using UnityEngine;
using System.Collections;
using System;

public class PassArrayToShader : MonoBehaviour
{

    // Note we're exploiting some sort of Unity "magic" whereby by appending an integer
    // to a uniform array name selects the specific array element based on that integer.

    // Unity 5.4 and above provides an interface which renders this class unnecessary.

    // For more info: http://www.alanzucconi.com/2016/01/27/arrays-shaders-heatmaps-in-unity3d/

    // (It would be nice to use generics here, but unfortunately Unity itself does not
    // provide a generic interface for setting a single parameter.)

    public static void Vector3(UnityEngine.Material material, string name, Vector3[] array)
    {
        for (int i = 0; i < array.Length; i++)
            material.SetVector(name + i.ToString(), array[i]);
    }

    public static void Color(UnityEngine.Material material, string name, Color[] array)
    {
        for (int i = 0; i < array.Length; i++)
            material.SetColor(name + i.ToString(), array[i]);
    }
}
