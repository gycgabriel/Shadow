using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderSelected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Image handle;

    public void OnSelect(BaseEventData eventData)
    {
        handle.color = new Color32(0, 119, 255, 255);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        handle.color = Color.white;
    }

    private void OnDisable()
    {
        handle.color = Color.white;
    }
}
