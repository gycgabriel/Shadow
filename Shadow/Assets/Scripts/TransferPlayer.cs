using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferPlayer : MonoBehaviour
{
    private static Player player;
    public string sceneToLoad;              // blank if same scene
    public string transferFrom;             // name of this TransferPlayer
    public string transferTo;               // move to TransferPlayer
    public Vector3 newCoords;               // or move to coords e.g. same scene/ one way teleports
    public Vector2 directionToFace;         // 0f, 0f if lastMove

    private static string nextTransferTo;    // spawn point in new scene
    private static Vector2 nextDirection;
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
                nextTransferTo = transferTo;
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

    private void Update()
    {
        // check if moved from TransferPlayer instance
        if (nextTransferTo != "" && transferFrom == nextTransferTo)
        {
            Vector3 oneStepAway = this.transform.position + new Vector3(nextDirection.x + offset.x, nextDirection.y + offset.y, 0f);      // so doesn't land on TransferPlayer
            Teleport(oneStepAway, nextDirection);
            nextTransferTo = "";
            nextDirection = Vector2.zero;
        }
    }

    // Teleport can be called from any script
    public static void Teleport(Vector3 coords, Vector2 direction)
    {
        player.GetComponent<PlayerController>().SetPosition(coords, direction);
    }


}
