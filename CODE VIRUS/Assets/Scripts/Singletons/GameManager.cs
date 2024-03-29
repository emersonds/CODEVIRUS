using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls points and multipliers, as well as handles multi-scene processes.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Reference to "scenes"
    private GameObject clickerScene;
    private GameObject simulationScene;

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
    [SerializeField]
    private float resilience = 0;
    [SerializeField]
    private float infectivity = 0;

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
    [SerializeField]
    private float baseResilienceRate = 0.1f;
    [SerializeField]
    private float baseInfectivityRate = 0.14f;

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

    // These values affect the chance a continent can be infected
    [SerializeField]
    private float mildBiome = 0.275f;
    [SerializeField]
    private float moderateBiome = 0.45f;
    [SerializeField]
    private float harshBiome = 0.6f;
    [SerializeField]
    private float infectThreshold = 0.55f;

    // Reference to virus
    private Virus virus;

    // Reference to Earth and continents
    private GameObject earth;
    // Infected continents
    [SerializeField]
    private List<Continent> infectedContinents = new List<Continent>();
    [SerializeField]
    private List<Continent> continents = new List<Continent>();

    // Reference to self
    public static GameManager GM;

    // Timer checks
    private bool pointsGeneratorRunning = false;
    private bool infectedGeneratorRunning = false;
    private bool deathGeneratorRunning = false;
    private bool infectNeighborRunning = false;

    // Used for easier scene checking
    private string currentScene;

    // Used for how frequently the game checks if it can infect
    [SerializeField]
    private float infectionStrength = 45f;

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

    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        currentScene = scene.name;

        if (scene.name == "Clicker")
        {
            // Assign game objects to "scene" objects
            if (clickerScene == null)
                clickerScene = GameObject.Find("Clicker");
            if (simulationScene == null)
                simulationScene = GameObject.Find("Simulation");
            if (virus == null)
                virus = GameObject.Find("Virus").GetComponent<Virus>();
            if (earth == null)
                earth = GameObject.Find("Earth");

            // Main menu loads simulation scene by default
            if (clickerScene.activeSelf)
            {
                clickerScene.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        clickerCost = baseClickerCost;
        infectCost = baseInfectCost;
        lethalCost = baseLethalCost;
        resilienceCost = baseResilienceCost;
        incomeCost = baseIncomeCost;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
                Application.Quit();
            else
                SceneManager.LoadScene("MainMenu");
        }
    }

    public void AddMutationPoints()
    {
        mutationPoints += (int)defaultClickValue;
    }

    public void UpgradeVirus(string upgrade)
    {
        switch (upgrade)
        {
            case ("infect"):
                if (mutationPoints - infectCost >= 0)
                {
                    // Check if timer running
                    if (!infectedGeneratorRunning)
                        StartCoroutine(GenerateInfected());

                    // Check if infect neighbor timer is running
                    if (!infectNeighborRunning)
                        StartCoroutine(InfectNeighbor());

                    // Cost
                    mutationPoints -= infectCost;
                    infectCounter++;
                    infectCost = Mathf.Ceil(baseInfectCost * Mathf.Pow(3, infectCounter));

                    // Upgrade
                    if (infectCounter < 9)
                        virus.ShowUpgrade(virus.Mushrooms, infectCounter - 1);

                    infectedPerMin = Mathf.Ceil((baseInfectedRate * infectCounter) * infectedMultiplier);

                    infectionStrength = Mathf.Clamp(infectionStrength - 1, 1, 15);

                    infectivity = Mathf.Clamp01(baseInfectivityRate * infectCounter);
                }
                break;
            case ("lethal"):
                if (mutationPoints - lethalCost >= 0)
                {
                    // Check if timer running
                    if (!deathGeneratorRunning)
                        StartCoroutine(GenerateDeath());

                    // Cost
                    mutationPoints -= lethalCost;
                    lethalCounter++;
                    lethalCost = Mathf.Ceil(baseLethalCost * Mathf.Pow(3, lethalCounter));

                    // Upgrade
                    if (lethalCounter < 9)
                        virus.ShowUpgrade(virus.Spikes, lethalCounter - 1);

                    deathsPerMin = Mathf.Ceil((baseDeathRate * lethalCounter) * deathMultiplier);
                }
                break;
            case ("resilience"):
                if (mutationPoints - resilienceCost >= 0)
                {
                    // Cost
                    mutationPoints -= resilienceCost;
                    resilienceCounter++;
                    resilienceCost = Mathf.Ceil(baseResilienceCost * Mathf.Pow(3, resilienceCounter));

                    // Upgrade
                    if (resilienceCounter < 9)
                        virus.ShowUpgrade(virus.Donuts, resilienceCounter - 1);

                    resilience = Mathf.Clamp01(baseResilienceRate * resilienceCounter);
                }
                break;
            case ("clicker"):
                if (mutationPoints - clickerCost >= 0)
                {
                    // Cost
                    mutationPoints -= clickerCost;
                    clickerCounter++;
                    clickerCost = Mathf.Ceil(baseClickerCost * Mathf.Pow(3, clickerCounter));

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
                    incomeCost = Mathf.Ceil(baseIncomeCost * Mathf.Pow(3, incomeCounter));

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
    /// Generates infected points/people every second
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateInfected()
    {
        infectedGeneratorRunning = true;

        // Loop runs forever
        for (; ; )
        {
            // Checks if there are any infected continents
            if (infectedContinents.Count > 0)
            {
                float newInfected = 0;
                for (int i = 0; i < infectedContinents.Count; i++)
                {
                    float tempInfected = Mathf.Clamp(infectedContinents[i].infectedCount + infectedPerMin, 0, infectedContinents[i].totalPopulation);
                    newInfected += tempInfected - infectedContinents[i].infectedCount;
                    infectedContinents[i].infectedCount = tempInfected;
                }

                // Add total amount of infected people to total infected points
                infectedPoints += newInfected;
            }

            // Wait one second before looping again
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
                // Checks if there are any infected continents
                if (infectedContinents.Count > 0)
                {
                    float newDeaths = 0;
                    float deadInfected = 0;
                    for (int i = 0; i < infectedContinents.Count; i++)
                    {
                        float tempDeaths = Mathf.Clamp(infectedContinents[i].deathCount + deathsPerMin, 0, infectedContinents[i].maxPopulation);
                        newDeaths += tempDeaths - infectedContinents[i].deathCount;
                        infectedContinents[i].totalPopulation = Mathf.Clamp(infectedContinents[i].totalPopulation - deathsPerMin, 0, infectedContinents[i].totalPopulation);
                        infectedContinents[i].infectedCount = Mathf.Clamp(infectedContinents[i].infectedCount - deathsPerMin, 0, infectedContinents[i].infectedCount);
                        infectedContinents[i].deathCount = tempDeaths;
                        deadInfected += tempDeaths - infectedContinents[i].deathCount;
                    }

                    // Add total amount of infected people to total infected points
                    deathPoints += newDeaths;
                    infectedPoints -= deadInfected;
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator InfectNeighbor()
    {
        // Set timer running
        infectNeighborRunning = true;

        // Initialize chanceToInfect variable
        float chanceToInfect = 0;

        // Used for calculating if a neighbor can be infected
        float biomeVal = 0;         // How harsh the climate is (resilience)

        if (infectedContinents.Count > 0)
        {
            for (; ; )
            {
                for (int i = 0; i < infectedContinents.Count; i++)
                {
                    for (int j = 0; j < infectedContinents[i].neighbors.Length; j++)
                    {
                        Continent currNeighbor = infectedContinents[i].neighbors[j].continent;

                        if (!currNeighbor.isInfected)
                        {

                            switch (currNeighbor.biome)
                            {
                                case ("mild"):
                                    biomeVal = mildBiome;
                                    break;

                                case ("moderate"):
                                    biomeVal = moderateBiome;
                                    break;

                                case ("harsh"):
                                    biomeVal = harshBiome;
                                    break;
                            }

                            chanceToInfect = Mathf.Clamp01(infectivity * ((2 * resilience) - biomeVal));

                            if (chanceToInfect > infectThreshold)
                            {
                                InfectContinent(currNeighbor.gameObject);
                                break;
                            }
                        }
                    }
                }

                yield return new WaitForSeconds(infectionStrength);
            }
        }
    }

    public void SetStartingContinent(GameObject continent)
    {
        startingContinentSelected = true;
        startingContinent = continent;
        InfectContinent(continent);

        Debug.Log("Starting continent selected: " + startingContinent.name);
    }

    public void InfectContinent(GameObject continent)
    {
        if (!infectedContinents.Contains(continent.GetComponent<Continent>()))
            infectedContinents.Add(continent.GetComponent<Continent>());

        continent.GetComponent<Continent>().isInfected = true;

        Debug.Log(continent.name + " has been infected.");
    }

    public void ChangeScenes(string scene)
    {
        switch(scene)
        {
            case ("Clicker"):
                clickerScene.SetActive(true);
                simulationScene.SetActive(false);
                break;

            case ("Simulation"):
                simulationScene.SetActive(true);
                clickerScene.SetActive(false);
                break;
        }
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
