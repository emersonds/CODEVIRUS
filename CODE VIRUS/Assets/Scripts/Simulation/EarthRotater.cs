using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotater : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 50f;

    private GameObject earth;
    private bool inverted;      // If the rotation should be inverted
    private int invertFactor;   // This is either -1 or 1 to flip the rotation
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
            Rotate("right");
        if (leftPressed)
            Rotate("left");
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
    
    void Rotate(string direction)
    {
        inverted = SettingsManager.SM.InvertEarthRot;

        if (!inverted)
            invertFactor = 1;
        else
            invertFactor = -1;

        Debug.Log("EARTH ROTATER\ninverted: " + inverted + "\ninvertFactor: " + invertFactor);

        switch(direction)
        {
            case ("right"):
                earth.transform.Rotate(new Vector3(0f, rotSpeed * invertFactor, 0f) * Time.deltaTime, Space.World);
                break;

            case ("left"):
                earth.transform.Rotate(new Vector3(0f, -rotSpeed * invertFactor, 0f) * Time.deltaTime, Space.World);
                break;
        }
    }

    public void ChangeScene(string scene)
    {
        GameManager.GM.ChangeScenes(scene);
    }
}
