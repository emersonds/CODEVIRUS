using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinentDisplayer : MonoBehaviour
{
    [SerializeField]
    private Text continentTitle;
    [SerializeField]
    private Text continentInfo;
    [SerializeField]
    private float updaterTimeToWait;

    private GameObject currentContinent;
    private bool updaterRunning;

    private void Start()
    {
        if (!updaterRunning)
            StartCoroutine(InfoUpdater());
    }

    public void SetContinentDisplay(GameObject continent)
    {
        Debug.Log("SetContinentDisplay called with continent: " + continent.GetComponent<Continent>().continentName);

        currentContinent = continent;

        // Set continent title
        continentTitle.text = continent.GetComponent<Continent>().continentName;

        // Set all continent info
        string population = GameManager.GM.GetSuffix(continent.GetComponent<Continent>().totalPopulation);
        string biome = continent.GetComponent<Continent>().biome;
        string infectedPopulation = GameManager.GM.GetSuffix(continent.GetComponent<Continent>().infectedCount);
        string deadPopulation = GameManager.GM.GetSuffix(continent.GetComponent<Continent>().deathCount);
        string infectedStatus;
        string neighbors = "";

        // Get continent infected status
        if (continent.GetComponent<Continent>().isInfected)
            infectedStatus = "Yes";
        else
            infectedStatus = "No";

        // Get all neighbor names
        for (int i = 0; i < continent.GetComponent<Continent>().neighbors.Length; i++)
        {
            if (neighbors != "")
                neighbors = neighbors + ", " + continent.GetComponent<Continent>().neighbors[i].continent.continentName;
            else
                neighbors = continent.GetComponent<Continent>().neighbors[i].continent.continentName;
        }

        // Display continent info
        continentInfo.text = "Population: " + population + "\n" +
                             "Biome: " + biome + "\n" +
                             "Infected: " + infectedStatus + "\n" +
                             "Infected Population: " + infectedPopulation + "\n" +
                             "Virus-Related Deaths: " + deadPopulation + "\n" +
                             "Neighbors: " + neighbors;
    }

    public void EnableContinentDisplay(bool enable)
    {
        gameObject.SetActive(enable);

        if (!enable)
        {
            StopCoroutine(InfoUpdater());
            updaterRunning = false;
        }
        else if (enable && !updaterRunning)
        {
            StartCoroutine(InfoUpdater());  // Coroutine sets updaterRunning to true
        }
    }

    private IEnumerator InfoUpdater()
    {
        updaterRunning = true;
        for(; ; )
        {
            if (gameObject.activeSelf)
                if (currentContinent != null)
                    SetContinentDisplay(currentContinent);
            yield return new WaitForSeconds(updaterTimeToWait);
        }
    }
}
