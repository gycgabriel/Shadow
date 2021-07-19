using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Modified from Copy properties of a class to another
 * https://stackoverflow.com/a/36713403 
 */

public static class CopyAllTo
{
    public static void CopyTo<T, U>(this T source, U target)
    {
        var fromType = typeof(T);
        var toType = typeof(U);
        foreach (var sourceProperty in fromType.GetProperties())
        {
            var targetProperty = toType.GetProperty(sourceProperty.Name);
            if (targetProperty != null)
                targetProperty.SetValue(target, sourceProperty.GetValue(source, null), null);
        }
        foreach (var sourceField in fromType.GetFields())
        {
            var targetField = toType.GetField(sourceField.Name);
            if (targetField != null)
                targetField.SetValue(target, sourceField.GetValue(source));
        }
    }
}
