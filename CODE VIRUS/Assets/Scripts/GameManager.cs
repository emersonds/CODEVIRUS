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

    // Upgrade variables
    private int infectCounter = 0;      // Used for showing mushrooms
    private int lethalCounter = 0;      // Used for showing spikes
    private int resilienceCounter = 0;  // Used for showing donuts

    // Reference to virus
    private Virus virus;

    private void Awake()
    {
        // Singleton stuff
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
                infectCounter++;
                break;
            case ("lethal"):
                // Upgrade stuff

                // Show part
                virus.ShowUpgrade(virus.Spikes, lethalCounter);
                lethalCounter++;
                break;
            case ("resilience"):
                // Upgrade stuff

                // Show part
                virus.ShowUpgrade(virus.Donuts, resilienceCounter);
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
