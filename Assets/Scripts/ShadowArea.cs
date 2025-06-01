using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ShadowArea : MonoBehaviour
{
    public float timer;
    public GameObject teleporter;
    public GameObject panel;
    public Text text;
    
    private Light2D playerLight;
    private Light2D globalLight;
    private Player player;
    private bool spoken;
    
    private void Start()
    {
        player = Player.Instance;
        globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        playerLight = player.GetComponent<Light2D>();
    }

    private void Update()
    {
        if (!player.canMove)
            return;
        if (timer is < 5 and > 3)
        {
            panel.SetActive(true);
            text.text = "Что-то мне нехорошо...";
            player.ChangeSpeed(2f);
            playerLight.pointLightOuterRadius = 1.5f;
        }
        if (timer < 3 && !spoken)
        {
            panel.SetActive(false);
            player.ChangeSpeed(1f);
            playerLight.pointLightOuterRadius = 1.1f;
            spoken = true;
        }
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            teleporter.SetActive(true);
        }
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
