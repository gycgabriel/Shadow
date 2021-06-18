using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferSpot : MonoBehaviour
{
    public string spotName; 

    void Update()
    {
        // check if moved from TransferPlayer instance
        if (TransferPlayer.nextTransferSpot == spotName)
        {
            TransferPlayer.Teleport(this.transform.position, TransferPlayer.nextDirection);
            TransferPlayer.nextTransferSpot = "";
            TransferPlayer.nextDirection = Vector2.zero;
        }
    }
}
