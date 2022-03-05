using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField]
    private GameObject continentDisplayer;

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
                continentDisplayer.GetComponent<ContinentDisplayer>().SetContinentDisplay(hit.collider.gameObject);
                continentDisplayer.GetComponent<ContinentDisplayer>().EnableContinentDisplay(true);
            }
        }
    }
}
