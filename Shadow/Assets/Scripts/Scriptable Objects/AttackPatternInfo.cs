using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Template for storing information for the different types of enemies.
 */
[CreateAssetMenu(fileName = "New AttackPatternInfo", menuName = "AttackPatternInfo")]
public class AttackPatternInfo : ScriptableObject
{
    public List<string> attackPattern;
}
