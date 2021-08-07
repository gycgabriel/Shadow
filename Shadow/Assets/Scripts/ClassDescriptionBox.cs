using TMPro;
using UnityEngine;

public class ClassDescriptionBox : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descText;

    public void DisplayClassInfo(ClassInfo classInfo)
    {
        nameText.text = classInfo.className;
        descText.text = classInfo.classDesc;
    }
}
