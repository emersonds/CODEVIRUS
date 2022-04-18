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
    [SerializeField]
    GameObject creditsPanel;

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
    [SerializeField]
    private Toggle invertEarthToggle;

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

        // Set default settings values
        SetDefaultVals();
    }

    public void GoToMenu(string menu)
    {
        switch(menu)
        {
            case ("main"):
                GoToMainMenu();
                break;

            case ("play"):
                SceneManager.LoadScene("Simulation");
                break;

            case ("options"):
                GoToOptions();
                break;

            case ("instructions"):
                GoToInstructions();
                break;

            case ("credits"):
                GoToCredits();
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
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        currentScene = "Main Menu";
    }

    void GoToCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        currentScene = "Credits";
    }

    /// <summary>
    /// Set the default values for slider elements
    /// </summary>
    void SetDefaultVals()
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
    /// Handles toggle behavior
    /// </summary>
    /// <param name="toggle">The toggle that was selected</param>
    public void ToggleClicked(Toggle toggle)
    {
        switch(toggle.name)
        {
            case("EarthRotationToggle"):
                SettingsManager.SM.InvertEarthRotation(toggle);
                break;
        }
    }

    /// <summary>
    /// Changes text value of a slider to match its value.
    /// </summary>
    /// <param name="slider">Which slider has changed.</param>
    public void ValueChangeCheck(Slider slider)
    {
        SettingsManager.SM.VolumeChanged(slider);

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

    public void VisitCredit(string credit)
    {
        switch(credit)
        {
            case ("arrows"):
                Application.OpenURL("https://thoseawesomeguys.com/prompts/");
                break;
            case ("skyboxes"):
                Application.OpenURL("https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633");
                break;
            case ("outline"):
                Application.OpenURL("https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488");
                break;
        }
    }
}
