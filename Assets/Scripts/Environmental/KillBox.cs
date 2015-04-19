using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
