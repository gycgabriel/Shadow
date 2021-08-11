using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGlow : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject buttonGlowImage;

    public void OnSelect(BaseEventData eventData)
    {
        buttonGlowImage.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonGlowImage.SetActive(false);
    }

    private void OnDisable()
    {
        buttonGlowImage.SetActive(false);
    }
}
