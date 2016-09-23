using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages Audiocall 
/// need audio? Call: GameManager.instance._audiManager.Play("name of audio in audio map in resources map" + AudioSource);
/// </summary>
public class AudioManager : MonoBehaviour
{
    public List<AudioObject> Audio_Objects = new List<AudioObject>();
    public AudioSettings[] audioSettings;
    private AudioSource _audioSource;

    public static AudioManager Instance;

    public class AudioObject
    {
        public string key;
        public AudioClip audioClip;
    }

    /// <summary>
    /// Applies the singleton pattern.
    /// </summary>
    private void ApplySingleton()
    {
        //Singleton 
        if (Instance == null)
            Instance = this;

        if (this != Instance)
            Destroy(gameObject);
    }
    public void Awake()
    {
        ApplySingleton();
        setupAudio();
    }

    /// <summary>
    /// Fills audioSettings List from audio's available in the Resources map.
    /// instantiates _audioSource.
    /// </summary>
    void setupAudio()
    {
        _audioSource = GetComponent<AudioSource>();

        //Fill audioSettings List from audio's available in the Resources map.
        foreach (AudioClip audioClip in Resources.LoadAll("Audio"))
        {
            //Makes new audio setting
            AudioObject audioSetting = new AudioObject();
            audioSetting.key = audioClip.name;
            audioSetting.audioClip = audioClip;

            //Finally add the setting to the list
            Audio_Objects.Add(audioSetting);
        }
    }

    public void Play(AudioSource audioSource, string key, bool isLooping = false, bool randomPitch = false)
    {
        AudioClip audioClip = Audio_Objects.Find(item => item.key == key).audioClip;
        audioSource.clip = audioClip;
        audioSource.loop = isLooping;

        if (randomPitch)
        {
            audioSource.pitch = Random.Range(0.8f, 1.5f);
        }
        else
        {
            audioSource.pitch = 1f;

        }

        audioSource.Play();
    }
}
