using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager AM;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// This is being used to assign sound information to an audio source
    /// </summary>
    void Awake()
    {
        // Checks if an AudioManager instance exists and destroys it
        // Singleton pattern to ensure the first AudioManager loaded is the one that persists
        if (AM == null)
        {
            AM = this;                                                // sets current AudioManager object as instance if no other exists
        }
        else
        {
            Destroy(gameObject);                                            // destroys self if there already exists another instance; breaks loop
            return;
        }

        // Makes the AudioManager game object persistent through multiple scenes
        DontDestroyOnLoad(gameObject);

        // Adds an audio source, clip, volume, pitch, and if the clip loops for every sound in array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();              // adds an audio source
            s.source.clip = s.clip;                                         // assigns the sound's clip to the source

            s.source.volume = s.volume;                                     // assigns the sound's volume to the source
            s.source.pitch = s.pitch;                                       // assigns the sound's pitch to the source
            s.source.loop = s.loop;                                         // assigns if the sound should loop to the source
        }
    }

    /// <summary>
    /// Plays a given audio clip
    /// This can be called in any script with "FindObjectOfType<AudioManager>().Play("AudioClip")"
    /// if it is attached to an AudioManager GameObject
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);          // searches for the sound in the array
        if (s == null)                                                      // prevents exceptions being thrown if unable to locate a sound
        {
            Debug.LogWarning("Sound: " + name + " not found!");             // prints a warning to console with which sound cannot be found
            return;
        }
        s.source.Play();                                                    // plays the sound on the audio source
    }

    /// <summary>
    /// Cuts off an audio clip
    /// </summary>
    /// <param name="name">name of the audio clip to stop</param>
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);          // searches for the sound in the array
        if (s == null)                                                      // prevents exceptions being thrown if unable to locate a sound
        {
            Debug.LogWarning("Sound: " + name + " not found!");             // prints a warning to console with which sound cannot be found
            return;
        }
        s.source.Stop();
    }

    /// <summary>
    /// Look for a sound and return if that sound is playing
    /// </summary>
    /// <param name="name"></param>
    /// <returns>true if sound is playing, otherwise false</returns>
    public bool PlayingSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);          // searches for the sound in the array
        if (s == null)                                                      // prevents exceptions being thrown if unable to locate a sound
        {
            Debug.LogWarning("Sound: " + name + " not found!");             // prints a warning to console with which sound cannot be found
            return false;
        }

        if (s.source.isPlaying)
        {
            Debug.Log("Sound " + name + " is playing!");
            return true;
        }
        else
        {
            Debug.Log("Sound " + name + " is not playing!");
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public void ChangeVolume(string name, float val)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);          // searches for the sound in the array
        if (s == null)                                                      // prevents exceptions being thrown if unable to locate a sound
        {
            Debug.LogWarning("Sound: " + name + " not found!");             // prints a warning to console with which sound cannot be found
            return;
        }
        s.source.volume = val;                                                    // Changes the sound's volume
    }
}
