using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferSpot : MonoBehaviour
{
    public string spotName;
    public bool offsetx;
    public bool offsety;

    void Update()
    {
        // check if moved from TransferPlayer instance
        if (TransferPlayer.nextTransferSpot == spotName)
        {
            Vector3 pos = this.transform.position;
            if (offsetx)
            {
                pos.x += TransferPlayer.offset.x;
            } else if (offsety)
            {
                pos.y += TransferPlayer.offset.y;
            }
            TransferPlayer.Teleport(pos, TransferPlayer.nextDirection);
            TransferPlayer.nextTransferSpot = "";
            TransferPlayer.nextDirection = Vector2.zero;
        }
    }
}
