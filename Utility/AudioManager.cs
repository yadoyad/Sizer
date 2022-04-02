using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
    public float soundFadeTime = 0.5f;
    public Sound[] sounds;
    public static AudioManager instance;
    public bool soundsEnabled {get; private set;}= true;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        SetupSounds();
        Application.targetFrameRate = -1;
    }

    private void Start() 
    {
        soundsEnabled = SaveLoadCtrl.instance.userData.soundsEnabled;
    }

    private void SetupSounds()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        if(soundsEnabled)
        {
            Sound s = Array.Find(sounds, sound => sound.Name == name);

            if(s.Name == null)
                return;

            s.source.Play();
        }
    }

    public void EnableSounds()
    {
        soundsEnabled = true;
        SaveLoadCtrl.instance.UpdateSoundsData(soundsEnabled);
    }

    public void DisableSounds()
    {
        soundsEnabled = false;
        SaveLoadCtrl.instance.UpdateSoundsData(soundsEnabled);
    }

    public void FadeOut(string name)
    {
        if(soundsEnabled)
        {
            Sound s = Array.Find(sounds, sound => sound.Name == name);

            if(s.Name == null)
                return;

            StartCoroutine(FadeOutRoutine(s, soundFadeTime)); 
        }
    }

    IEnumerator FadeOutRoutine(Sound s, float fadeTime)
    {
        float startVolume = s.source.volume;

        while (s.source.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        s.source.Stop();
        s.source.volume = startVolume;
    }
}
