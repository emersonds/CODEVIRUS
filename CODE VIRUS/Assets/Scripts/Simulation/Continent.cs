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
    public float maxPopulation;
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
    [SerializeField]
    Material infectionMaterial;
    [SerializeField]
    Material deathMaterial;

    private Material baseMaterial;

    private void Start()
    {
        gameObject.GetComponent<Outline>().enabled = false;
        baseMaterial = GetComponent<Material>();
    }

    private void Update()
    {

    }

    public void SetSelected(bool selected)
    {
        gameObject.GetComponent<Outline>().enabled = selected;
    }
}
