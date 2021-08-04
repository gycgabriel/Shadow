using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : Singleton<MinimapCameraController>
{
    public Transform player;

    void LateUpdate()
    {
        if (PartyController.activePC == null)
            return;

        player = PartyController.activePC.transform;
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
