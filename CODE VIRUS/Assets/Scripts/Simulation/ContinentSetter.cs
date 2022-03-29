using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinentSetter : MonoBehaviour
{
    [SerializeField]
    private GameObject startingPanel;

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

        // Allow the player to swap between the clicker and simulation screens
        GameObject.Find("RotateMenu").GetComponent<EarthRotater>().EnableVirusButton();
    }
}
