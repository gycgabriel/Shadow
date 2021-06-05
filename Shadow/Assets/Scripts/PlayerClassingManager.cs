using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerClassingManager : MonoBehaviour
{
    public GameObject TheOther;                                        // Player or shadow to switch to

    public enum CharacterClass { Guardian, Sorcerer, Kannagi };     // List of all possible character classes

    public static string[] portraits = new string[] { "portrait_guardian", "portrait_sorcerer", "portrait_kannagi" };                     // Until we get class Guardian

    public CharacterClass playerCharaClass;                         // Player's character class
    public Sprite playerPortrait;                                   // Player default portrait before choosing class
    public bool shadowMode;                                         // if this is shadow gameObject / don't rly need but might be good to know in future

    // Start is called before the first frame update
    void Start()
    {
        ChoosePlayerClass(playerCharaClass);                    // for testing, until character creation screen created
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Async Sprite Loading
    public void LoadSprite(AsyncOperationHandle<Sprite> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            playerPortrait = handle.Result;
        }
    }


    public void ChoosePlayerClassFromButton(string value)
    {
        System.Enum.TryParse(value, out CharacterClass myClass);
        ChoosePlayerClass(myClass);
    }

    // Choose character classes
    public void ChoosePlayerClass(CharacterClass value)
    {
        // TODO: Add sprite change, skill change, stat formula change
        playerCharaClass = value;
        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(portraits[(int) value]);       // loads into memory for future instantiation
        handle.Completed += LoadSprite;
    }
    public void ToggleShadowMode()
    {
        // TODO: Replace all instances of (Find component by id) ThePlayer with whichever Shadow or Player is active
        TheOther.SetActive(true);
        this.gameObject.SetActive(false);
    }

    

}
