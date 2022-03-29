using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls points and multipliers, as well as handles multi-scene processes.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Points
    [SerializeField]
    private float defaultClickValue = 1;
    [SerializeField]
    private float mutationPoints = 0;
    [SerializeField]
    private float mutationPointsPerMin = 0;
    [SerializeField]
    private float mutationPointsMultiplier = 1;
    [SerializeField]
    private float infectedPoints = 0;
    [SerializeField]
    private float infectedPerMin = 0;
    [SerializeField]
    private float infectedMultiplier = 1;
    [SerializeField]
    private float deathPoints = 0;
    [SerializeField]
    private float deathsPerMin = 0;
    [SerializeField]
    private float deathMultiplier = 1;

    // Public references to private data
    public float MutationPoints { get { return mutationPoints; } }
    public float InfectedPoints { get { return infectedPoints; } }
    public float DeathPoints { get { return deathPoints; } }

    // Base production rate of income-based upgrades
    [SerializeField]
    private float baseMutationPointsRate = 3.59f;
    [SerializeField]
    private float baseInfectedRate = 5.6f;
    [SerializeField]
    private float baseDeathRate = 2.7f;


    // Base cost of upgrades
    [SerializeField]
    private float baseInfectCost = 5;         // Base upgrade cost
    [SerializeField]
    private float baseLethalCost = 5;         // Base upgrade cost
    [SerializeField]
    private float baseResilienceCost = 5;     // Base upgrade cost
    [SerializeField]
    private float baseClickerCost = 5;        // Base upgrade cost
    [SerializeField]
    private float baseIncomeCost = 5;         // Base upgrade cost

    // Public references to private data
    public float InfectCost { get { return infectCost; } }
    public float LethalCost { get { return lethalCost; } }
    public float ResilienceCost { get { return resilienceCost; } }
    public float ClickerCost { get { return clickerCost; } }
    public float IncomeCost { get { return incomeCost; } }

    // First time playing data
    private bool startingContinentSelected = false;
    private GameObject startingContinent;

    // Public references to starting data
    public bool StartingContinentSelected { get { return startingContinentSelected; } }
    public GameObject StartingContinent { get { return startingContinent; } }

    // How many of an upgrade the players owns
    private int infectCounter = 0;
    private int lethalCounter = 0;
    private int resilienceCounter = 0;
    private int clickerCounter = 0;
    private int incomeCounter = 0;

    // How much an upgrade costs
    private float infectCost;
    private float lethalCost;
    private float resilienceCost;
    private float clickerCost;
    private float incomeCost;

    // Reference to virus
    private Virus virus;

    // Reference to self
    public static GameManager GM;

    // Timer checks
    private bool pointsGeneratorRunning = false;
    private bool infectedGeneratorRunning = false;
    private bool deathGeneratorRunning = false;

    private void Awake()
    {
        // Checks if a GameManager instance exists and destroys it
        // Singleton pattern to ensure the first GameManager loaded is the one that persists
        if (GM == null)
        {
            GM = this;                                                // sets current GameManager object as instance if no other exists
        }
        else
        {
            Destroy(gameObject);                                            // destroys self if there already exists another instance; breaks loop
            return;
        }

        // Makes the GameManager game object persistent through multiple scenes
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        //virus = GameObject.Find("Virus").GetComponent<Virus>();       // Instantiating GameManager from main menu makes this throw an error
        clickerCost = baseClickerCost;
        infectCost = baseInfectCost;
        lethalCost = baseLethalCost;
        resilienceCost = baseResilienceCost;
        incomeCost = baseIncomeCost;
    }

    // Update is called once per frame
    private void Update()
    {
        if (virus == null &&
            SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Clicker"))
        {
            virus = GameObject.Find("Virus").GetComponent<Virus>();
            Debug.Log("Virus Assigned");
            MakeUpgradesVisible(virus.Mushrooms, infectCounter);
            MakeUpgradesVisible(virus.Spikes, lethalCounter);
            MakeUpgradesVisible(virus.Donuts, resilienceCounter);
        }

        AddMutationPoints("Passive");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
                Application.Quit();
            else
                SceneManager.LoadScene("MainMenu");
        }
    }

    public void AddMutationPoints(string source)
    {
        switch (source)
        {
            case ("Click"):
                mutationPoints += (int)defaultClickValue;
                break;

            case ("Passive"):
                //mutationPoints += (int)((mutationPointsPerMin * Time.deltaTime) * mutationPointsMultiplier);
                break;
        }
    }

    public void UpgradeVirus(string upgrade)
    {
        switch (upgrade)
        {
            case ("infect"):
                if (infectCounter < 9 && (mutationPoints - infectCost >= 0))
                {
                    // Check if timer running
                    if (!infectedGeneratorRunning)
                        StartCoroutine(GenerateInfected());

                    // Cost
                    mutationPoints -= infectCost;
                    infectCounter++;
                    infectCost = Mathf.Ceil(baseInfectCost * Mathf.Pow(4, infectCounter));

                    // Upgrade
                    virus.ShowUpgrade(virus.Mushrooms, infectCounter - 1);
                    infectedPerMin = Mathf.Ceil((baseInfectedRate * infectCounter) * infectedMultiplier);
                }
                break;
            case ("lethal"):
                if (lethalCounter < 9 && (mutationPoints - lethalCost >= 0))
                {
                    // Check if timer running
                    if (!deathGeneratorRunning)
                        StartCoroutine(GenerateDeath());

                    // Cost
                    mutationPoints -= lethalCost;
                    lethalCounter++;
                    lethalCost = Mathf.Ceil(baseLethalCost * Mathf.Pow(4, lethalCounter));

                    // Upgrade
                    virus.ShowUpgrade(virus.Spikes, lethalCounter - 1);
                    deathsPerMin = Mathf.Ceil((baseDeathRate * lethalCounter) * deathMultiplier);
                }
                break;
            case ("resilience"):
                if (resilienceCounter < 9 && (mutationPoints - resilienceCost >= 0))
                {
                    // Cost
                    mutationPoints -= resilienceCost;
                    resilienceCounter++;
                    resilienceCost = Mathf.Ceil(baseResilienceCost * Mathf.Pow(4, resilienceCounter));

                    // Upgrade
                    virus.ShowUpgrade(virus.Donuts, resilienceCounter - 1);

                }
                break;
            case ("clicker"):
                if (mutationPoints - clickerCost >= 0)
                {
                    // Cost
                    mutationPoints -= clickerCost;
                    clickerCounter++;
                    clickerCost = Mathf.Ceil(baseClickerCost * Mathf.Pow(4, clickerCounter));

                    // Upgrade
                    defaultClickValue *= 2;
                }
                break;
            case ("income"):
                if (mutationPoints - incomeCost >= 0)
                {
                    // Check if timer running
                    if (!pointsGeneratorRunning)
                        StartCoroutine(GenerateMP());

                    // Cost
                    mutationPoints -= incomeCost;
                    incomeCounter++;
                    incomeCost = Mathf.Ceil(baseIncomeCost * Mathf.Pow(4, incomeCounter));

                    // Upgrade
                    mutationPointsPerMin = Mathf.Ceil((baseMutationPointsRate * incomeCounter) * mutationPointsMultiplier);
                }
                break;
        }
    }

    /// <summary>
    /// Determines how a number should be printed.
    /// This should display 1-999999, 1M, 1B, 1T, etc.
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public string GetSuffix(float num)
    {
        string value = "";

        // Check which suffix to use
        if (num <= 999999)
            value = num.ToString();
        else if (num > 999999 && num <= 999999999)
            value = (num / 1000000f).ToString("0.##") + " M";
        else if (num > 999999999 && num <= 999999999999)
            value = (num / 1000000000f).ToString("0.##") + " B";
        else if (num > 999999999999)
            value = (num / 1000000000000f).ToString("0.##") + " T";

        return value;
    }

    /// <summary>
    /// Generates mutation points
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateMP()
    {
        pointsGeneratorRunning = true;
        for (; ; )
        {
            mutationPoints += mutationPointsPerMin;
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Generates infected points/people
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateInfected()
    {
        infectedGeneratorRunning = true;
        for (; ; )
        {
            infectedPoints += infectedPerMin;
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Generates DEATH.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateDeath()
    {
        deathGeneratorRunning = true;
        for (; ; )
        {
            if (infectedPoints - deathsPerMin >= 0)
            {
                Debug.Log("Deaths have occurred.\nDeaths Per Min: " + deathsPerMin +
                    "\nInfected Points: " + infectedPoints + "\nDeath Points: " + deathPoints);
                infectedPoints -= deathsPerMin;
                deathPoints += deathsPerMin;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void SetStartingContinent(GameObject continent)
    {
        startingContinentSelected = true;
        startingContinent = continent;

        Debug.Log("Starting continent selected: " + startingContinent.name);
    }

    public void ChangeScenes(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Temporary method to make sure Virus keeps showing its upgrades on scene changes.
    /// This is temporary until a save/load feature is implemented.
    /// </summary>
    /// <param name="arr">Virus part</param>
    /// <param name="counter">Array Index: Which part to show.</param>
    private void MakeUpgradesVisible(GameObject[] arr, int counter)
    {
        if (counter > 0)
        {
            for (int i = 0; i < counter; i++)
            {
                virus.ShowUpgrade(arr, i);
            }
        }
    }
}
