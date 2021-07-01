using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SkillSet : MonoBehaviour
{
    public SkillInfo ultimateSkill;
    abstract public void NormalAttack();
    abstract public void UltimateSkill();
}
