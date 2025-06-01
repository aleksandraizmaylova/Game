using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ShadowArea : MonoBehaviour
{
    public float timer;
    public Text timerText;
    public GameObject teleporter;
    public GameObject panel;
    public Text text;
    
    private Light2D playerLight;
    private Light2D globalLight;
    private Player player;
    private bool spoken;
    private bool timerStarted;
    
    private void Start()
    {
        player = Player.Instance;
        globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        playerLight = player.GetComponent<Light2D>();
    }

    private void Update()
    {
        if (!player.canMove || !timerStarted)
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
        {
            timer -= Time.deltaTime;
            timerText.text = $"{(int)timer}";
        }
        else
        {
            teleporter.SetActive(true);
            timerText.text = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timerStarted = true;
            globalLight.enabled = false;
            playerLight.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.ChangeSpeed(Constants.NormalSpeed);
            playerLight.enabled = false;
            globalLight.enabled = true;
            timer = 0;
        }
    }
}
