using UnityEngine;
using System.Collections;

public class orangePortal : MonoBehaviour
{
    public bool playerEntered = false;
    public bool blueExists = false;

    GameObject bluePortal;

    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal");
        if (bluePortal != null)
        {
            blueExists = true;
        }
        else
        {
            blueExists = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (bluePortal != null)
        {
            print("not null!");
            if (other.tag == "Player" || other.tag == "PuzzleEntity")
            {
                other.transform.position = bluePortal.transform.position + bluePortal.transform.forward * 2;

                //If player walks through a portal that is on a wall, rotate them to face forward
                if (other.tag == "Player")
                {
                    player.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                }
            }
        }
    }
}
