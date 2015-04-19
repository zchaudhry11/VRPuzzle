using UnityEngine;
using System.Collections;

public class PortalEntry : MonoBehaviour 
{

    public float timer = 1.5f;

    GameObject blue;
    GameObject orange;

	void Update () 
    {
        blue = GameObject.FindGameObjectWithTag("BluePortal");
        orange = GameObject.FindGameObjectWithTag("OrangePortal");
	}

    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } 
        if (timer < 0)
        {
            timer = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (blue != null && orange != null && timer <= 0)
        {
            if (col.gameObject.tag == "OrangePortal" || col.gameObject.tag == "PuzzleEntity") //teleport to blue
            {
                Vector3 bluePos = blue.transform.position;
                bluePos.y += 3;
                //Debug.DrawLine(transform.position, bluePos, Color.red, 100);
                if (col.tag == "PuzzleEntity")
                {
                    col.transform.position = bluePos;
                    col.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                }
                else
                {
                    transform.position = bluePos;
                    transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                }
            }
            if (col.gameObject.tag == "BluePortal" || col.gameObject.tag == "PuzzleEntity") //teleport to orange
            {
                Vector3 orangePos = orange.transform.position;
                orangePos.y += 3;
                //Debug.DrawLine(transform.position, orangePos, Color.red, 100);

                if (col.tag == "PuzzleEntity")
                {
                    col.transform.position = orangePos;
                    col.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                }
                else
                {
                    transform.position = orangePos;
                    transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                }
            }
            timer = 1.5f;
        }
    }
}
