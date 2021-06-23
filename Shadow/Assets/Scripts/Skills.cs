using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Skills : MonoBehaviour
{
    public Skill ultimateSkill;
    abstract public void NormalAttack();
    abstract public void UltimateAttack();
}
