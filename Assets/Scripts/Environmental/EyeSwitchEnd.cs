using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EyeSwitchEnd : MonoBehaviour 
{
    GameObject[] instances;

    int flag = 0;

    void Update()
    {
        instances = GameObject.FindGameObjectsWithTag("EyeSwitch");

        for (int i = 0; i < instances.Length; i++)
        {
            if (instances[i].GetComponent<EyeSwitchHealth>().activated == false)
            {
                this.GetComponent<LevelEnd>().canFinishLevel = false;
            }
            else
            {
                flag++;
            }

            if (flag == instances.Length)
            {
                this.GetComponent<LevelEnd>().canFinishLevel = true;
            }
            else
            {
                flag = 0;
                this.GetComponent<LevelEnd>().canFinishLevel = false;
            }
        }
    }
}
