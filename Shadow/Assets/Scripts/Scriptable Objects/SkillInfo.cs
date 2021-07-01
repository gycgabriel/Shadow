using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Template for storing information for the different types of enemies.
 */
[CreateAssetMenu(fileName = "New SkillInfo", menuName = "SkillInfo")]
public class SkillInfo : ScriptableObject
{
    public new string name;
    public Sprite skillIcon;
    public int skillMPCost;
    public float skillCooldown;
}
