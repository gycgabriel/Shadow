using UnityEngine;

// Makes game object appear in the middle of certain scenarios
public class AppearWhileScenario : MonoBehaviour
{
    public int afterChapter;
    public int afterScenario;
    public int beforeChapter;
    public int beforeScenario;

    public GameObject gameObj;  // Example: Minimap blocked path icons

    // Update is called once per frame
    void Update()
    {
        if (StoryManager.scriptInstance.CheckEvoked(afterChapter, afterScenario) && !StoryManager.scriptInstance.CheckEvoked(beforeChapter, beforeScenario))
            gameObj.SetActive(true);
        else
            gameObj.SetActive(false);
    }
}
