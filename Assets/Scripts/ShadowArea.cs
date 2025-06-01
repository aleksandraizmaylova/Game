using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowArea : MonoBehaviour
{
    private Light2D playerLight;
    private Light2D globalLight;
    
    private void Start()
    {
        globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        playerLight = Player.Instance.GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            globalLight.enabled = false;
            playerLight.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerLight.enabled = false;
            globalLight.enabled = true;
        }
    }
}
