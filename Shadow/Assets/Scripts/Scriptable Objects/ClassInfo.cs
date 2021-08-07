using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Template for storing information for the different character classes.
 */
[CreateAssetMenu(fileName = "New ClassInfo", menuName = "ClassInfo")]
public class ClassInfo : ScriptableObject
{
    public string className;

    [TextArea(3, 10)]
    public string classDesc;
}
