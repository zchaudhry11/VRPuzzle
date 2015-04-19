using UnityEngine;
using System.Collections;

public class LevelEnd1 : MonoBehaviour
{
    GameObject player;

    GameObject door;

    EyeSwitchEnd eyeswitch;

    public bool canFinishLevel = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        eyeswitch = this.GetComponent<EyeSwitchEnd>();
    }

    void Update()
    {
        if (door == null && eyeswitch == null)
        {
            canFinishLevel = true;
        }
        else
        {
            if (door != null)
            {
                if (door.GetComponent<Door>().isActive == true)
                {
                    canFinishLevel = true;
                }
                else
                {
                    canFinishLevel = false;
                }
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Application.LoadLevel("LevelTwo");
        }
    }
}
