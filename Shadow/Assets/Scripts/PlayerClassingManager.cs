using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassingManager : MonoBehaviour
{
    public GameObject TheOther;                                        // Player or shadow to switch to

    public enum CharacterClass { Guardian, Sorcerer, Kannagi };     // List of all possibel character classes
    public CharacterClass playerCharaClass;                         // Player's character class
    public bool shadowMode;                                         // if this is shadow gameObject / don't rly need but might be good to know in future

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Choose character classes
    public void ChoosePlayerClass(CharacterClass value)
    {
        // TODO: Add sprite change, skill change, stat formula change
        playerCharaClass = value;
    }
    public void ToggleShadowMode()
    {
        // TODO: Replace all instances of (Find component by id) ThePlayer with whichever Shadow or Player is active
        TheOther.SetActive(true);
        this.gameObject.SetActive(false);
    }



}
