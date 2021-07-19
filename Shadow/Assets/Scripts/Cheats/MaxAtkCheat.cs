using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAtkCheat : MonoBehaviour
{
    void Update()
    {
        if (PartyController.scriptInstance != null && Input.GetKeyDown(KeyCode.F))
        {
            PartyController.playerP.stats.addBaseStat("atk", 999);
            PartyController.playerP.stats.addBaseStat("matk", 999);
            PartyController.shadowP.stats.addBaseStat("atk", 999);
            PartyController.shadowP.stats.addBaseStat("matk", 999);
        }
    }
}
