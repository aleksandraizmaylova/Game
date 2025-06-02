using System;
using UnityEngine;
using UnityEngine.UI;

public class Grave : MonoBehaviour
{
    [Header("Sound Settings")]
    public AudioClip breakSound;

    public GameObject panel;
    public Text text;
    public string[] lines;
    public Animator animator;
    
    private Player player;
    private int lineCounter;
    private bool isMonologueActive;
    private bool isPlayerNear;
    private AudioSource audioSource;


    void Start()
    {
        player = Player.Instance;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

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
            animator.SetTrigger("Break");
            PlayBreakSound();
        }
    }
    private void PlayBreakSound()
    {
        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
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
