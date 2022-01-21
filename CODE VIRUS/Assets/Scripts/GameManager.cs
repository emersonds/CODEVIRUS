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
    private int defaultClickValue;
    [SerializeField]
    private int mutationPoints;
    [SerializeField]
    private int mutationPointsPerMin;
    [SerializeField]
    private float mutationPointsMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintPoints());
    }

    // Update is called once per frame
    void Update()
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
                mutationPoints += (int)((mutationPointsPerMin * Time.fixedDeltaTime) * mutationPointsMultiplier);
                break;
        }
    }

    /// <summary>
    /// Temporary coroutine until UI is created.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PrintPoints()
    {
        Debug.Log("Current points: " + mutationPoints);
        yield return new WaitForSeconds(2f);
    }
}
