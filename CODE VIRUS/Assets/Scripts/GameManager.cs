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
    private int mutationPointsPerMin = 0;
    [SerializeField]
    private float mutationPointsMultiplier = 1;
    [SerializeField]
    private int infectedPerMin = 0;
    [SerializeField]
    private float infectedMultiplier = 1;
    [SerializeField]
    private int deathsPerMin = 0;
    [SerializeField]
    private float deathMultiplier = 0;

    private int mutationPoints = 0;
    private int infectedPoints = 0;
    private int deathPoints = 0;

    public int MutationPoints { get { return mutationPoints; } }

    // Upgrade variables
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
    }

    public void AddMutationPoints(string source)
    {
        switch(source)
        {
            case ("Click"):
                mutationPoints += (int)(defaultClickValue * mutationPointsMultiplier);
                break;

            case ("Passive"):
                mutationPoints += (int)((mutationPointsPerMin * Time.deltaTime) * mutationPointsMultiplier);
                break;
        }
    }

    public void UpgradeVirus(string upgrade)
    {
        switch (upgrade)
        {
            case ("infect"):
                // Upgrade stuff

                // Show part
                virus.ShowUpgrade(virus.Mushrooms, infectCounter);
                if (infectCounter < 10)
                    infectCounter++;
                break;
            case ("lethal"):
                // Upgrade stuff

                // Show part
                virus.ShowUpgrade(virus.Spikes, lethalCounter);
                if (lethalCounter < 10)
                    lethalCounter++;
                break;
            case ("resilience"):
                // Upgrade stuff

                // Show part
                virus.ShowUpgrade(virus.Donuts, resilienceCounter);
                if (resilienceCounter < 10)
                    resilienceCounter++;
                break;
            case ("clicker"):
                // Upgrade stuff

                break;
            case ("income"):
                // Upgrade stuff

                break;
        }
    }
}
