using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    public bool isActive = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PuzzleEntity")
        {
            isActive = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "PuzzleEntity")
        {
            isActive = false;
        }
    }
}
