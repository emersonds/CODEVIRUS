using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    // Default values
    [SerializeField]
    private float defaultMusicVolume = 0.2f;
    [SerializeField]
    private float defaultSFXVolume = 0.7f;
    [SerializeField]
    private float defaultUIVolume = 0.5f;
    [SerializeField]
    private bool defaultInvertEarthRot = false;

    // Public access to default values
    public float DefaultMusicVolume { get { return defaultMusicVolume; } }
    public float DefaultSFXVolume { get { return defaultSFXVolume; } }
    public float DefaultUIVolume { get { return defaultUIVolume; } }
    public bool DefaultInvertEarthRot { get { return defaultInvertEarthRot; } }

    // Reference to self
    public static SettingsManager SM;

    private void Awake()
    {
        // Checks if a GameManager instance exists and destroys it
        // Singleton pattern to ensure the first GameManager loaded is the one that persists
        if (SM == null)
        {
            SM = this;                                                // sets current GameManager object as instance if no other exists
        }
        else
        {
            Destroy(gameObject);                                      // destroys self if there already exists another instance; breaks loop
            return;
        }

        // Makes the GameManager game object persistent through multiple scenes
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Set default sound volume
        AudioManager.AM.ChangeVolume("Music", DefaultMusicVolume);
        AudioManager.AM.ChangeVolume("SFX", DefaultSFXVolume);
        AudioManager.AM.ChangeVolume("UI", DefaultUIVolume);
    }

    /// <summary>
    /// Changes the current volume of sounds throughout the game.
    /// </summary>
    /// <param name="slider">Volume slider object from options menu.</param>
    public void VolumeChanged(Slider slider)
    {
        if (GameObject.Find("MainMenu Canvas") != null)
        {
            if (GameObject.Find("MainMenu Canvas").GetComponent<MainMenu>().CurrentScene == "Options")
            {
                switch (slider.name)
                {
                    // Change music volume
                    case ("MusicSlider"):
                        AudioManager.AM.ChangeVolume("Music", slider.value);
                        break;

                    // Changed sound effects volume
                    case ("SFXSlider"):
                        AudioManager.AM.ChangeVolume("SFX", slider.value);
                        AudioManager.AM.Play("Virus Click");
                        break;

                    // Change volume of UI elements
                    case ("UISlider"):
                        AudioManager.AM.ChangeVolume("UI", slider.value);
                        AudioManager.AM.Play("Button Hover");
                        break;

                    // Slider doesn't exist or hasn't been implemented above
                    default:
                        Debug.Log(slider.name + " does not exist!");
                        break;
                }
            }
        }
    }
}
