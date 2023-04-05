using System;
using Random = UnityEngine.Random;

public static class RandomEnumExtension
{
    public static T NextEnum<T>(int startIndex = 0)
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(startIndex, values.Length));
    }
}