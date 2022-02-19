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


    // Start is called before the first frame update
    void Start()
    {
        GoToMainMenu();
        AudioManager.AM.Play("Main Theme");
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
    }

    void GoToInstructions()
    {
        optionsPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    void GoToMainMenu()
    {
        optionsPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
