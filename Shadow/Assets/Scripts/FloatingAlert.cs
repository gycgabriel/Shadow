using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script for animating the floating damage numbers
public class FloatingAlert : MonoBehaviour
{
    public float moveSpeed;             //How fast the damage numbers float up 
    
    private SpriteRenderer alertSprite;

    public float alertTime;
    private float alertTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        alertSprite = GetComponent<SpriteRenderer>();
        alertTimeCounter = alertTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Move the damage numbers upwards according to the moveSpeed
        transform.Translate(new Vector3(0f, (moveSpeed * Time.deltaTime), 0f));
        alertSprite.color = new Color(1f, 1f, 1f, alertTimeCounter/alertTime);
        alertTimeCounter -= Time.deltaTime;
    }
}
