using UnityEngine;

// Makes the game object or sprite appear while a certain quest is active
public class AppearWhileQuest : MonoBehaviour
{
    public Quest quest;

    public GameObject gameObj;

    void Update()
    {
        if (PartyController.scriptInstance == null)
            return;

        if (gameObj != null)
        {
            if (PartyController.quest != null && PartyController.quest.title == quest.title && PartyController.quest.isActive)
                gameObj.SetActive(true);
            else
                gameObj.SetActive(false);
        }
        else
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (PartyController.quest != null && PartyController.quest.title == quest.title && PartyController.quest.isActive)
                sr.color = new Color(sr.color[0], sr.color[1], sr.color[2], 1);
            else
                sr.color = new Color(sr.color[0], sr.color[1], sr.color[2], 0);
        }
        
    }

}
