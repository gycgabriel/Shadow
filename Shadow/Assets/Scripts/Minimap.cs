using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        player = PartyController.activePC.transform;
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
