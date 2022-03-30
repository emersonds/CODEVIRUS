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
    public float totalPopulation;
    [SerializeField]
    public float infectedCount;
    [SerializeField]
    public float deathCount;
    [SerializeField]
    public string biome;
    [SerializeField]
    public Neighbor[] neighbors;
}
