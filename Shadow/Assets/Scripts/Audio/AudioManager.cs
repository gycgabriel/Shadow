using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using System;

/**
 * To play a sound, 
 * AudioManager.scriptInstance.PlaySFX("name")
 * To replace a bgm, 
 * AudioManager.scriptInstance.PlayBGM("name")
 */

public class AudioManager : Singleton<AudioManager>
{
    // Volume settings 0 to 10
    public int totalVolume;
    public int bgmVolume;
    public int sfxVolume;

    public Sound[] sfxSounds;

    public Sound currentBGM;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    void Start()
    {
        totalVolume = (int) PlayerPrefs.GetFloat("tvol", 10f);
        bgmVolume = (int) PlayerPrefs.GetFloat("bgmvol", 10f);
        sfxVolume = (int) PlayerPrefs.GetFloat("sfxvol", 10f);

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;      // if bgm dont loop then what are you
        bgmSource.priority = 0;     // use 0 for music tracks to avoid it being swapped out // documentation
    }

    void Update()
    {
        // Garbage collection
        if (sfxSource != null && !sfxSource.isPlaying)
            Destroy(sfxSource);

        // Update volume
        if (sfxSource == null && currentBGM != null)
            bgmSource.volume = currentBGM.volume * bgmVolume / 10 * totalVolume / 10;
    }

    public void PlayBGM(Sound sound)
    {
        // Same song, do nothing
        if (currentBGM != null && currentBGM.name == sound.name)
            return;

        if (currentBGM != null)
        {
            bgmSource.Stop();
        }

        currentBGM = sound;
        bgmSource.clip = sound.clip;
        bgmSource.pitch = sound.pitch;
        bgmSource.volume = sound.volume * bgmVolume / 10 * totalVolume / 10;
        bgmSource.Play();
    }


    public AudioSource PlaySFX(string name)
    {
        float prevVolume = 0f;
        // Soften current BGM
        if (currentBGM != null)
        {
            prevVolume = bgmSource.volume;
            bgmSource.volume = bgmSource.volume - 0.2f > 0f ? bgmSource.volume - 0.2f : 0.0f;
        }

        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " was not found.");
            return null;
        }

        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = s.clip;
        source.pitch = s.pitch;
        source.loop = s.loop;
        source.volume = s.volume * sfxVolume / 10 * totalVolume / 10;
        source.Play();
        sfxSource = source;

        if (prevVolume != 0f)
            StartCoroutine(UnsoftenVolume(sfxSource, prevVolume));

        return source;
    }


    IEnumerator UnsoftenVolume(AudioSource source, float prevVolume)
    {
        yield return new WaitUntil(() => !source.isPlaying);

        bgmSource.volume = prevVolume;

        Destroy(source);
    }
}
