using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBehaviour : MonoBehaviour
{
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void save(int saveNum)
    {
        SaveSystem.savePlayer(player.gameObject, saveNum);
    }
}
