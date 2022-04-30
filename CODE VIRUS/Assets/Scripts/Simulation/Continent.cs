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
    private Material baseMaterial;
    [SerializeField]
    private Material infectionMaterial;

    private Renderer rend;

    private void Start()
    {
        gameObject.GetComponent<Outline>().enabled = false;
        rend = GetComponent<Renderer>();

        rend.material = baseMaterial;
    }

    private void Update()
    {
        rend.material.Lerp(baseMaterial, infectionMaterial, (infectedCount / totalPopulation));
    }

    public void SetSelected(bool selected)
    {
        gameObject.GetComponent<Outline>().enabled = selected;
    }
}
