using UnityEngine;
using System.Collections;

public class VRState : MonoBehaviour 
{
    public bool onSpecialFloor = false;

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "FloorEntity")
        {
            onSpecialFloor = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "FloorEntity")
        {
            onSpecialFloor = false;
        }
    }
}
