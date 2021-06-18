using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferPlayer : MonoBehaviour
{
    private static Player player;
    public string sceneToLoad;              // blank if same scene
    public string transferTo;
    public Vector3 newCoords;               // blank if have spot name
    public Vector2 directionToFace;         // 0f, 0f if lastMove

    public static string nextTransferSpot;
    public static Vector2 nextDirection;
    private static Vector3 offset;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (sceneToLoad != "")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            if (transferTo != "")
            {
                nextTransferSpot = transferTo;
                offset = player.GetComponent<PlayerController>().movePoint.transform.position - this.transform.position;    // step on which part of teleporter
                if (directionToFace == Vector2.zero)
                {
                    nextDirection = player.GetComponent<PlayerController>().lastMove;
                } 
                else
                {
                    nextDirection = directionToFace;
                }
            } 
            else
            {
                Teleport(newCoords, directionToFace);
            }
            
        }
    }

    // Teleport can be called from any script
    public static void Teleport(Vector3 coords, Vector2 direction)
    {
        player.GetComponent<PlayerController>().SetPosition(coords, direction);
    }


}
