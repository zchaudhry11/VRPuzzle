using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour 
{
	void Update () 
    {
	    if (Input.GetButtonDown("Level1"))
        {
            Application.LoadLevel("LevelOne");
        }
        if (Input.GetButtonDown("Level2"))
        {
            Application.LoadLevel("LevelTwo");
        }
        if (Input.GetButtonDown("Level3"))
        {
            Application.LoadLevel("LevelFive");
        }
	}
}
