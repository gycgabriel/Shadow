using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    public MinotaurAI boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        boss.StartBossFight();
    }
}
