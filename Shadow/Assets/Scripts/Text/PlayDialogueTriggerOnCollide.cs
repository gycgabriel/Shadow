using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Attach to GO with Dialogue Trigger
 */
public class PlayDialogueTriggerOnCollide : MonoBehaviour
{
    private bool inCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && inCollision == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                this.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
            inCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && inCollision == true)
        {
            inCollision = false;
        }
    }
}
