using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** 
 * Parent class for gameObjects that can only exist one at a time.
 * Only inherit in child scripts attached to root GameObject! DontDestroyOnLoad requirement.
 * NOTE: DO NOT HAVE AWAKE() IN CHILDREN OF THIS CLASS ( it will override singleton behaviour ) 
 */

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T scriptInstance;
    public static GameObject gameInstance;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameInstance == null && this.gameObject != gameInstance)
        {
            this.gameObject.DontDestroyOnLoad();
            scriptInstance = this as T;
            gameInstance = this.gameObject;
        }
        else
        {
            this.Destroy();
        }
    }

    // Convenience method
    // Player has to destroy movepoints and other things so virtual for overriding
    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }

    /**
     * When making another instance the singleton instead
     */
    public void Reset()
    {
        Destroy();
        scriptInstance = null;
        gameInstance = null;
    }

}
