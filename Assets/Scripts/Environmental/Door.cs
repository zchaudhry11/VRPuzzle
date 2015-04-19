using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour 
{
    public bool isActive = true;
    public GameObject[] requiredButtons;

    List<bool> buttonActivity = new List<bool>();

    void Update()
    {
        if (requiredButtons[0].GetComponent<Button>().isActive == true)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
}
