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
            }
            else
            {
                activePC = playerPC;
            }

            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            bool attackInput = Input.GetKeyDown(KeyCode.J);
            activePC.HandleInput(movement, attackInput);
        }

    }

}
