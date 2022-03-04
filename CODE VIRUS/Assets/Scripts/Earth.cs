using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        Click();
    }

    /// <summary>
    /// Checks for player clicks and returns what continent was clicked
    /// </summary>
    private void Click()
    {
        // Check for click input
        if (Input.GetMouseButtonDown(0))
        {
            // Create ray at mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Checks which continent was clicked if the ray hit a continent
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.collider.transform.name)
                {
                    case ("Africa"):
                        Debug.Log("Africa clicked");
                        break;
                    case ("Australia"):
                        Debug.Log("Australia clicked");
                        break;
                    case ("Asia"):
                        Debug.Log("Asia clicked");
                        break;
                    case ("Europe"):
                        Debug.Log("Europe clicked");
                        break;
                    case ("North America"):
                        Debug.Log("North America clicked");
                        break;
                    case ("South America"):
                        Debug.Log("South America clicked");
                        break;
                    default:
                        Debug.Log(hit.collider.transform.name);
                        break;
                }
            }
        }
    }
}
