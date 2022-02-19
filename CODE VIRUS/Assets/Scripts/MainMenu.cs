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

    private string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        GoToMainMenu();
        if (!AudioManager.AM.PlayingSound("Main Theme"))
            AudioManager.AM.Play("Main Theme");

        musicVolumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck("music"); });
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

    void ValueChangeCheck(string slider)
    {
        float value = 0f;   // Used to display the slider value

        switch(slider)
        {
            // If method was called in an unintended way
            default:
                break;

            case ("music"):
                value = Mathf.Round((musicVolumeSlider.value / musicVolumeSlider.maxValue) * 100f);
                musicVolumeValue.text = value.ToString() + "%";
                AudioManager.AM.ChangeVolume("Music", musicVolumeSlider.value);
                break;
        }
    }
}
