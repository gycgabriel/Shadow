using TMPro;
using UnityEngine;

//Script for animating the floating text
public class FloatingText : MonoBehaviour
{
    public float moveSpeed;             //How fast the text float up 
    public string text;                 //The text to be displayed
    public TMP_Text displayText;            //The Text object displaying the damage value

    // Start is called before the first frame update
    void Start()
    {
        //Set the Text value to the text
        if (text != null)
            displayText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        //Move the text upwards according to the moveSpeed
        transform.Translate(new Vector3(0f, (moveSpeed * Time.deltaTime), 0f));
    }
}
