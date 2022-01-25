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

    private void Awake()
    {
        // Singleton stuff
    }

    // Start is called before the first frame update
    private void Start()
    {

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
}
