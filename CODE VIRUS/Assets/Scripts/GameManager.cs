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
    private float deathMultiplier = 0;

    public float MutationPoints { get { return mutationPoints; } }
    public float InfectedPoints { get { return infectedPoints; } }
    public float DeathPoints { get { return deathPoints; } }


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
    }

    // Update is called once per frame
    private void Update()
    {
        if(virus == null &&
            SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Clicker"))
        {
            virus = GameObject.Find("Virus").GetComponent<Virus>();
            Debug.Log("Virus Assigned");
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
                    // Cost
                    mutationPoints -= infectCost;
                    infectCounter++;
                    infectCost = Mathf.Ceil(baseInfectCost * Mathf.Pow(4, infectCounter));

                    // Upgrade
                    virus.ShowUpgrade(virus.Mushrooms, infectCounter - 1);
                    
                }
                break;
            case ("lethal"):
                if (lethalCounter < 9 && (mutationPoints - lethalCost >= 0))
                {
                    // Cost
                    mutationPoints -= lethalCost;
                    lethalCounter++;
                    lethalCost = Mathf.Ceil(baseLethalCost * Mathf.Pow(4, lethalCounter));

                    // Upgrade
                    virus.ShowUpgrade(virus.Spikes, lethalCounter - 1);
                    
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
                // Upgrade stuff
                
                break;
        }
    }
}
