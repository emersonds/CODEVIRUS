using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotater : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 50f;

    private GameObject earth;
    private bool rightPressed = false;
    private bool leftPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        earth = GameObject.Find("Earth");
    }

    private void FixedUpdate()
    {
        if (rightPressed)
            earth.transform.Rotate(new Vector3(0f, -rotSpeed, 0f) * Time.deltaTime, Space.World);
        if (leftPressed)
            earth.transform.Rotate(new Vector3(0f, rotSpeed, 0f) * Time.deltaTime, Space.World);
    }

    public void Rotater(string direction)
    {
        Debug.Log("Rotater called");
        switch(direction)
        {
            case ("right"):
                rightPressed = true;
                break;
            case ("left"):
                leftPressed = true;
                break;
        }
    }

    public void UnRotater(string direction)
    {
        Debug.Log("UnRotater called");
        switch (direction)
        {
            case ("right"):
                rightPressed = false;
                break;
            case ("left"):
                leftPressed = false;
                break;
        }
    }
}
