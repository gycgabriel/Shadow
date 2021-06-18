using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : Singleton<PartyController>
{
    public static bool shadowActive = false;
    public static GameObject player;
    public static GameObject shadow;
    public static PlayerController playerPC;
    public static PlayerController shadowPC;
    public static PlayerController activePC;
    public static PlayerController inactivePC;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.childCount <= 0)
        {
            return;
        }

        // init
        if (player == null || shadow == null)
        {
            player = transform.GetChild(0).gameObject;
            shadow = transform.GetChild(1).gameObject;
        }
        else if (playerPC == null || shadowPC == null)
        {
            playerPC = player.GetComponent<PlayerController>();
            shadowPC = shadow.GetComponent<PlayerController>();
        }
        else
        {
            if (shadowActive)
            {
                activePC = shadowPC;
                inactivePC = playerPC;
            }
            else
            {
                activePC = playerPC;
                inactivePC = shadowPC;
            }

            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            bool attackInput = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J);
            bool switchToShadowInput = Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Q);
            inactivePC.SetPosition(activePC.transform.position, activePC.lastMove);
            activePC.HandleInput(movement, attackInput, switchToShadowInput);
        }

    }

    public static void switchShadow()
    {
        if (shadowActive)
        {
            shadow.SetActive(false);
            player.SetActive(true);
        }
        else
        {
            shadow.SetActive(true);
            player.SetActive(false);
        }
        shadowActive = !shadowActive;
    }


    public override void Destroy()
    {
        playerPC.Destroy();
        shadowPC.Destroy();
    }
}
