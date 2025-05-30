using System;
using UnityEngine;
using UnityEngine.UI;

public class Monologue : MonoBehaviour
{
    public GameObject panel;
    public Text text;
    public string[] lines;
    
    private Player player;
    private int lineCounter;
    private bool isMonologueActive;

    private void Start()
    {
        player = Player.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartMonologue();
    }

    private void StartMonologue()
    {
        if (lineCounter > 0)
            return;
        player.canMove = false;
        panel.SetActive(true);
        isMonologueActive = true;
        NextLine();
    }

    void Update()
    {
        if (isMonologueActive && Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        if (lineCounter < lines.Length)
        {
            text.text = lines[lineCounter];
            lineCounter++;
        }
        else
        {
            panel.SetActive(false);
            isMonologueActive = false;
            player.canMove = true;
        }
    }
}
