using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Multiple scenarios in one chapter, arbituary defined by length and release
 */

[System.Serializable]

public class Chapter
{
    public int id;
    public Scenario[] data;
}
