using System;
using UnityEngine;
using UnityEngine.UI;

public class Grave : MonoBehaviour
{
    public GameObject panel;
    public Text text;
    public string[] lines;
    public GameObject fragment;
    public GameObject grave;
    
    
    private Player player;
    private int lineCounter;
    private bool isMonologueActive;
    private bool isPlayerNear;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNear)
            NextLine();
    }
    
    private void NextLine()
    {
        if (!isMonologueActive)
        {
            player.canMove = false;
            panel.SetActive(true);
            isMonologueActive = true;
        }
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
            fragment.SetActive(true);
            grave.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
            isPlayerNear = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
            isPlayerNear = false;
    }
}
