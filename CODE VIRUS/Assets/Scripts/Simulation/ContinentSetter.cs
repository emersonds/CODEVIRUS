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
        GameObject.Find("RotateMenu").GetComponent<EarthRotater>().EnableVirusButton();
    }
}
