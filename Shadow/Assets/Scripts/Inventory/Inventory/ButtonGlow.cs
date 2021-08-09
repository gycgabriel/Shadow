using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGlow : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject buttonGlowImage;

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Selected " + name);
        buttonGlowImage.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Deselected " + name);
        buttonGlowImage.SetActive(false);
    }

    private void OnDisable()
    {
        Debug.Log("Disabled " + name);
        buttonGlowImage.SetActive(false);
    }
}
