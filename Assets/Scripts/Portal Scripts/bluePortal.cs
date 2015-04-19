using UnityEngine;
using System.Collections;

public class bluePortal : MonoBehaviour 
{
    public bool playerEntered = false;
    public bool orangeExists = false;

    GameObject orangePortal;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        orangePortal = GameObject.FindGameObjectWithTag("OrangePortal");
        if (orangePortal != null)
        {
            orangeExists = true;
        }
        else
        {
            orangeExists = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (orangePortal != null)
        {
            print("not null");
            if (other.tag == "Player" || other.tag == "PuzzleEntity")
            {
                print("Entered");
                other.transform.position = orangePortal.transform.position + orangePortal.transform.forward * 2;

                //If player walks through a portal that is on a wall, rotate them to face forward
                if (other.tag == "Player")
                {
                    player.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                }
            }
        }
    }
}
