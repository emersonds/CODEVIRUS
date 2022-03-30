using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinentSetter : MonoBehaviour
{
    [SerializeField]
    private GameObject startingPanel;
    [SerializeField]
    private GameObject startHereButton;

    private void Start()
    {
        if (!GameManager.GM.StartingContinentSelected)
            startingPanel.SetActive(true);
        else
            startingPanel.SetActive(false);
    }

    public void CloseStartingPanel()
    {
        startingPanel.SetActive(false);
    }

    public void SetStartingContinent()
    {
        // Get the currently selected continent
        GameObject startingContinent = GameObject.Find("ContinentInfo").GetComponent<ContinentDisplayer>().CurrentContinent;

        // Set it as the starting continent
        GameManager.GM.SetStartingContinent(startingContinent);

        // Hide "Start Here" button
        startHereButton.SetActive(false);

        // Allow the player to swap between the clicker and simulation screens
        GameObject.Find("RotateMenu").GetComponent<EarthRotater>().EnableVirusButton();
    }
}
