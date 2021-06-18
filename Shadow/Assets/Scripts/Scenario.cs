using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Multiple dialogues, bunch of text before next other action.
 */

[System.Serializable]
public class Scenario
{
    public int id;
    public Dialogue[] data;
}
