using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffUI()
    {
        transform.root.GetComponent<Canvas>().gameObject.SetActive(false);
        Time.timeScale = 1f;
    }




}


