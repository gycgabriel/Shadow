using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]

public class Sound : ScriptableObject
{
    public new string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    [Range(-3f, 3f)]
    public float pitch = 1f;

    public bool loop = false;
}
