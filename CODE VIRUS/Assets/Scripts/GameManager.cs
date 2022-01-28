using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls points and multipliers, as well as handles multi-scene processes.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Points
    [SerializeField]
    private int defaultClickValue = 1;
    [SerializeField]
    private int mutationPoints = 0;
    [SerializeField]
    private int mutationPointsPerMin = 0;
    [SerializeField]
    private float mutationPointsMultiplier = 1;
    [SerializeField]
    private int infectedPoints = 0;
    [SerializeField]
    private int infectedPerMin = 0;
    [SerializeField]
    private float infectedMultiplier = 1;
    [SerializeField]
    private int deathPoints = 0;
    [SerializeField]
    private int deathsPerMin = 0;
    [SerializeField]
    private float deathMultiplier = 0;

    public int MutationPoints { get { return mutationPoints; } }
    public int InfectedPoints { get { return infectedPoints; } }
    public int DeathPoints { get { return deathPoints; } }


    // Upgrade variables
    [SerializeField]
    private int infectCost = 5;         // Base upgrade cost
    [SerializeField]
    private int lethalCost = 5;         // Base upgrade cost
    [SerializeField]
    private int resilienceCost = 5;     // Base upgrade cost
    [SerializeField]
    private int clickerCost = 5;        // Base upgrade cost
    [SerializeField]
    private int incomeCost = 5;         // Base upgrade cost

    public int InfectCost { get { return infectCost; } }
    public int LethalCost { get { return lethalCost; } }
    public int ResilienceCost { get { return resilienceCost; } }
    public int ClickerCost { get { return clickerCost; } }
    public int IncomeCost { get { return incomeCost; } }

    private int infectCounter = 0;      // Used for showing mushrooms
    private int lethalCounter = 0;      // Used for showing spikes
    private int resilienceCounter = 0;  // Used for showing donuts

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
        virus = GameObject.Find("Virus").GetComponent<Virus>();
    }

    // Update is called once per frame
    private void Update()
    {
        AddMutationPoints("Passive");
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void AddMutationPoints(string source)
    {
        switch (source)
        {
            case ("Click"):
                mutationPoints += defaultClickValue;
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
                    // Upgrade stuff
                    mutationPoints -= infectCost;
                    infectCost *= 2;

                    // Show part
                    virus.ShowUpgrade(virus.Mushrooms, infectCounter);
                    infectCounter++;
                }
                break;
            case ("lethal"):
                if (lethalCounter < 9 && (mutationPoints - lethalCost >= 0))
                {
                    // Upgrade stuff
                    mutationPoints -= lethalCost;
                    lethalCost *= 2;

                    // Show part
                    virus.ShowUpgrade(virus.Spikes, lethalCounter);
                    lethalCounter++;
                }
                break;
            case ("resilience"):
                if (resilienceCounter < 9 && (mutationPoints - resilienceCost >= 0))
                {
                    // Upgrade stuff
                    mutationPoints -= resilienceCost;
                    resilienceCost *= 2;

                    // Show part
                    virus.ShowUpgrade(virus.Donuts, resilienceCounter);
                    resilienceCounter++;
                }
                break;
            case ("clicker"):
                // Upgrade stuff
                if (mutationPoints - clickerCost >= 0)
                {
                    mutationPoints -= clickerCost;
                    clickerCost *= 2;
                    defaultClickValue *= 2;
                }
                break;
            case ("income"):
                // Upgrade stuff
                // PASSIVE INCOME NOT YET IMPLEMENTED SO DONT WORRY ABOUT THIS
                break;
        }
    }
}
