using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script for animating the floating damage numbers
public class FloatingNumbers : MonoBehaviour
{
    public float moveSpeed;             //How fast the damage numbers float up 
    public int damageNumber;            //The damage value to be displayed
    public Text displayNumber;          //The Text object displaying the damage value

    // Start is called before the first frame update
    void Start()
    {
        //Set the Text value to the damage value dealt
        displayNumber.text = damageNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Move the damage numbers upwards according to the moveSpeed
        transform.Translate(new Vector3(0f, (moveSpeed * Time.deltaTime), 0f));
    }
}
