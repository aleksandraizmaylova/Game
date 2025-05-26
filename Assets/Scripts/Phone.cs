using System;
using System.Collections;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public Animator animator;
    public GameObject newCanvas;
    public GameObject oldCanvas;
    public int phoneNumber = -1;
    public GameObject phone;
    public GameObject fragment;
    private int result;
    private Player player;
    private Vector2 position;

    private void Start()
    {
        player = Player.Instance;
        position= phone.transform.position + Vector3.down;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.canMove = false;
            oldCanvas.SetActive(false);
            newCanvas.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.canMove = true;
            newCanvas.SetActive(false);
            oldCanvas.SetActive(true);
        }
    }

    private void Update()
    {
        if (result == phoneNumber)
        {
            fragment.SetActive(true);
            phone.SetActive(false);
            result = -1;
            // player.transform.position = position;
        }
    }

    public void Push(int i)
    {
        animator.SetTrigger($"{i}");
        result = result * 10 + i;
        Debug.Log($"result: {result}");
    }
}
