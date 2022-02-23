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
    
    // Public reference to parts
    public GameObject[] Spikes { get { return spikes; } }
    public GameObject[] Mushrooms { get { return mushrooms; } }
    public GameObject[] Donuts { get { return donuts; } }

    // Rotation/Hovering Values
    [SerializeField]
    private float rotSpeed;         // How fast the virus rotates
    [SerializeField]
    private float amplitude;        // How high/low the virus moves
    [SerializeField]
    private float frequency;        // How fast the virus move up/down
    private Vector3 posOffset = new Vector3();  // Initial position
    private Vector3 tempPos = new Vector3();    // New position

    // Start is called before the first frame update
    private void Start()
    {
        // Store starting position for movement
        posOffset = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Click();        // Checks for player clicks
        Move();         // Virus moving and spinning animation
    }

    /// <summary>
    /// Checks for player clicks and increments points when the player clicks the virus
    /// </summary>
    private void Click()
    {
        // Check for click input
        if (Input.GetMouseButtonDown(0))
        {
            // Create ray at mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if ray hit the virus
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Virus")
                {
                    // Points stuff here ...
                    GameManager.GM.AddMutationPoints("Click");
                    AudioManager.AM.Play("Virus Click");
                    Debug.Log("Player clicked virus.");
                }
            }
        }
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

    /// <summary>
    /// Shows a part of the virus corresponding to an upgrade.
    /// </summary>
    /// <param name="arr">Virus part</param>
    /// <param name="counter">Array Index: Which part to show.</param>
    public void ShowUpgrade(GameObject[] arr, int counter)
    {
        // Checks if array is null before setting active the part at the index
        if (arr != null)
        {
            arr[counter].SetActive(true);
            AudioManager.AM.Play("Virus Upgrade");
        }
    }
}
