using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/**
 * PortraitBehaviour script to add to all gameObjects with portraits. 
 * Every character class has its own gameObject prefab, when instantiating playerSprite 
 * add this script and corresponding portraitPath.
 */
public class PortraitBehaviour : MonoBehaviour
{
    public string portraitPath;             // Addressable asset path to portrait 
    public Sprite portraitToDisplay;
    public Sprite spriteToDisplay;


    void Awake()
    {
        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(portraitPath);       // loads into memory for future instantiation
        handle.Completed += LoadSprite;
    }

    public void LoadSprite(AsyncOperationHandle<Sprite> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            portraitToDisplay = handle.Result;
        }
    }
}
