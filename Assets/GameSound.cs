using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{
    public bool debugMode = false;
    public static GameSound _gameSound;
    public float distanceFactor = 0.5f;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private AudioSource currentAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        InitializeGamesound();
    }
    private void InitializeGamesound()
    {
        _gameSound = this;
        for (int i = 0; i < 12; i++)
        {
            AudioSource newAs = gameObject.AddComponent<AudioSource>();
            audioSources.Add(newAs);
            if (!debugMode)
                newAs.hideFlags = HideFlags.HideInInspector;
        }
    }
    public void PlayDistantSound(float distance, float volume, AudioClip clip, bool shakeCamera = false)
    {
        float newVolume = volume * (1f - (distance / 200f) * (1f - (distance / 200f)));
        if (newVolume < 0.1f && shakeCamera == false)
            return;
        StartCoroutine(playDistantSound(distance, newVolume,  clip, shakeCamera));
    }
    public IEnumerator playDistantSound(float distance, float volume, AudioClip clip, bool shakeCamera = false)
    {
        yield return new WaitForSeconds(distance * distanceFactor);
        currentAudioSource = getFreeAudioSource();
        currentAudioSource.volume = volume * (1f - (distance / 200f)* (1f - (distance / 200f)));
        currentAudioSource.pitch = Random.Range(0.9f, 1.1f);
        currentAudioSource.clip = clip;
        currentAudioSource.Play();
     
        if (shakeCamera)
            if (distance < 80f)
                GameCamera.SmallShake();
    }

    public void PlaySimpleSound(float distance, float volume, AudioClip clip)
    {
        currentAudioSource = getFreeAudioSource();
        currentAudioSource.volume = volume * (1f - (distance / 300f) * (1f - (distance / 300f)));
        currentAudioSource.pitch = Random.Range(0.9f, 1.1f);
        currentAudioSource.clip = clip;
        currentAudioSource.Play();
    }
    private AudioSource getFreeAudioSource()
    {
        foreach (AudioSource a in audioSources)
        {
            if (a.isPlaying)
                continue;
            else
                return a;
        }
        return audioSources[audioSources.Count-1];
    }
}
