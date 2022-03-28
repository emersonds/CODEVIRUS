using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Menu Panels
    [SerializeField]
    GameObject mainMenuPanel;
    [SerializeField]
    GameObject optionsPanel;
    [SerializeField]
    GameObject instructionsPanel;

    // Options Menu
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Text musicVolumeValue;
    [SerializeField]
    private Slider sfxVolumeSlider;
    [SerializeField]
    private Text sfxVolumeValue;
    [SerializeField]
    private Slider uiVolumeSlider;
    [SerializeField]
    private Text uiVolumeValue;

    // The current menu being displayed
    private string currentScene;

    // Public reference to current scene
    public string CurrentScene { get { return currentScene; } }

    // Start is called before the first frame update
    void Start()
    {
        // Displays the main menu
        GoToMainMenu();

        // Plays the main theme if it's not currently playing
        if (!AudioManager.AM.PlayingSound("Main Theme"))
            AudioManager.AM.Play("Main Theme");

        // Set default slider values
        SetSliderVals();
    }

    public void GoToMenu(string menu)
    {
        switch(menu)
        {
            case ("main"):
                GoToMainMenu();
                break;

            case ("play"):
                SceneManager.LoadScene("Clicker");
                break;

            case ("options"):
                GoToOptions();
                break;

            case ("instructions"):
                GoToInstructions();
                break;

            case ("quit"):
                Application.Quit();
                break;
        }
    }

    void GoToOptions()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        optionsPanel.SetActive(true);
        currentScene = "Options";
    }

    void GoToInstructions()
    {
        optionsPanel.SetActive(false);
        instructionsPanel.SetActive(true);
        currentScene = "Instructions";
    }

    void GoToMainMenu()
    {
        optionsPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        currentScene = "Main Menu";
    }

    /// <summary>
    /// Set the default values for slider elements
    /// </summary>
    void SetSliderVals()
    {
        // Set default music slider elements
        musicVolumeSlider.value = SettingsManager.SM.DefaultMusicVolume;
        musicVolumeValue.text = Mathf.Round((musicVolumeSlider.value / musicVolumeSlider.maxValue) * 100f).ToString() + "%";

        // Set default SFX slider elements
        sfxVolumeSlider.value = SettingsManager.SM.DefaultSFXVolume;
        sfxVolumeValue.text = Mathf.Round((sfxVolumeSlider.value / sfxVolumeSlider.maxValue) * 100f).ToString() + "%";

        // Set default UI slider elements
        uiVolumeSlider.value = SettingsManager.SM.DefaultUIVolume;
        uiVolumeValue.text = Mathf.Round((uiVolumeSlider.value / uiVolumeSlider.maxValue) * 100f).ToString() + "%";
    }

    public void ButtonHover()
    {
        AudioManager.AM.Play("Button Hover");
    }

    public void ButtonClick()
    {
        AudioManager.AM.Play("Button Click");
    }

    /// <summary>
    /// Changes text value of a slider to match its value.
    /// </summary>
    /// <param name="slider">Which slider has changed.</param>
    public void ValueChangeCheck(Slider slider)
    {
        switch(slider.name)
        {
            // Change music slider displays
            case ("MusicSlider"):
                musicVolumeValue.text = Mathf.Round((musicVolumeSlider.value / musicVolumeSlider.maxValue) * 100f).ToString() + "%";
                break;
            // Change SFX slider displays
            case ("SFXSlider"):
                sfxVolumeValue.text = Mathf.Round((sfxVolumeSlider.value / sfxVolumeSlider.maxValue) * 100f).ToString() + "%";
                break;
            // Change UI Slider displays
            case ("UISlider"):
                uiVolumeValue.text = Mathf.Round((uiVolumeSlider.value / uiVolumeSlider.maxValue) * 100f).ToString() + "%";
                break;
            // Slider doesn't exist or hasn't been implemented above
            default:
                Debug.Log(slider.name + " does not exist!");
                break;
        }
    }
}
