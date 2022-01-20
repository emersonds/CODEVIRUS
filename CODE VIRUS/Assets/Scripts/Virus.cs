using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    // Parts of the virus
    [SerializeField]
    private GameObject[] spikes = new GameObject[10];
    [SerializeField]
    private GameObject[] mushrooms = new GameObject[10];
    [SerializeField]
    private GameObject[] donuts = new GameObject[10];

    // Rotation/Hovering Values
    [SerializeField]
    private float rotSpeed;         // How fast the virus rotates
    [SerializeField]
    private float amplitude;        // How high/low the virus moves
    [SerializeField]
    private float frequency;        // How fast the virus move up/down

    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        // Store starting position for movement
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Rotate the virus
        transform.Rotate(new Vector3(0f, rotSpeed, 0f) * Time.deltaTime, Space.World);

        // Move the virus
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
