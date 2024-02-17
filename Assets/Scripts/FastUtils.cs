using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class FastUtils
{
    public static bool Contains<T>(this List<T> list, T item, out int index, bool checkRef = true)
    {
        index = -1;

        if (checkRef)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (ReferenceEquals(list[i], item))
                {
                    index = 1;
                    return true;
                }
            }
            return false;
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(item))
            {
                index = 1;
                return true;
            }
        }

        return false;
    }

    public static bool Contains<T>(this T[] arr, T item, out int index, bool checkRef = true)
    {
        index = -1;

        if (checkRef)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (ReferenceEquals(arr[i], item))
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].Equals(item))
            {
                index = 1;
                return true;
            }
        }

        return false;
    }

    public static T Last<T>(this T[] arr)
    {
        return arr[arr.Length - 1];
    }

    public static T Last<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }

    public static void RemoveAtSwapBack<T>(this List<T> list, int index)
    {
        list[index] = list.Last();
        list.RemoveAt(list.Count - 1);
    }

    public static void SetLayerRecursive(this GameObject gameObject, int layer)
    {
        Transform[] carryTransforms = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in carryTransforms)
        {
            t.gameObject.layer = layer;
        }
    }
}
