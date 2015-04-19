using UnityEngine;
using System.Collections;

public class EyeSwitchHealth : MonoBehaviour 
{
    public float switchHealth = 100;
    public bool activated = false;
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Standard");
    }

    void Update()
    {
        if (switchHealth <= 0)
        {
            rend.material.SetColor("_Color", Color.cyan);
        }
    }

    public void RemoveHealth(int hp)
    {
        switchHealth -= hp;
    }
}
