using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continent : MonoBehaviour
{
    [SerializeField]
    public string continentName;
    [SerializeField]
    public bool isInfected = false;
    [SerializeField]
    public int totalPopulation;
    [SerializeField]
    public int infectedCount;
    [SerializeField]
    public int deathCount;
    [SerializeField]
    public string biome;
    [SerializeField]
    public Neighbor[] neighbors;

    private void Start()
    {
        if (neighbors.Length > 0 && neighbors[0] != null)
            Debug.Log(neighbors[0].continent.continentName);
        else
            Debug.Log("No neighbor!!");
    }
}
