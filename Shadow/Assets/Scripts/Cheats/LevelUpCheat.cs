using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpCheat : MonoBehaviour
{
    void Update()
    {
        if (PartyController.scriptInstance != null && Input.GetKeyDown(KeyCode.F))
        {
            PartyController.AddExperience(9000);
        }
    }
}
