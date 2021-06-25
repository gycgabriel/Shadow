using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBehaviour : MonoBehaviour
{
    private PartyController party;
    
    // Start is called before the first frame update
    void Start()
    {
        party = FindObjectOfType<PartyController>();
    }

    public void save(int saveNum)
    {
        SaveSystem.savePlayer(party.gameObject, saveNum);
    }
}
