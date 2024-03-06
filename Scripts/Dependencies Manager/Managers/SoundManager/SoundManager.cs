using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAudio();
    }

    public void RegisterSound(string id, AudioClip clip)
    {
        if (!audioClips.ContainsKey(id))
            audioClips.Add(id, clip);
        else
            Debug.LogWarning("Sound with ID " + id + " is already registered.");
    }

    public void PlaySound(string id)
    {
        if (audioClips.ContainsKey(id))
        {
            AudioSource audioSource = GetAvailableAudioSource();
            if (audioSource != null)
            {
                audioSource.clip = audioClips[id];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("No available audio sources to play the sound.");
            }
        }
        else
        {
            Debug.LogWarning("Sound with ID " + id + " not found.");
        }
    }

    public void StopSound(string id)
    {
        foreach (var source in audioSources)
        {
            if (source.clip != null && source.clip.name == id && source.isPlaying)
            {
                source.Stop();
                return;
            }
        }
        Debug.LogWarning("Sound with ID " + id + " is not playing.");
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (var source in audioSources)
        {
            if (!source.isPlaying)
                return source;
        }
        return null;
    }

    public void InitializeAudio()
    {
        RegisterSound("Countdown", Resources.Load<AudioClip>("Sounds/Countdown"));

        // Create a fixed number of AudioSources
        for (int i = 0; i < 5; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.volume = 0.3f;
            audioSources.Add(newSource);
        }
    }
}