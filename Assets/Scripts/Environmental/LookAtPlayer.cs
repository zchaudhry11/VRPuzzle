using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour 
{
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 200))
        {
            //Debug.DrawLine(transform.position, hit.point, Color.green, 100);
            if (hit.transform.gameObject.tag == "EyeSwitch")
            {
                hit.transform.gameObject.GetComponent<EyeSwitchHealth>().RemoveHealth(15);
            }
        }
    }
}
