using System;
using UnityEngine;

public class PlayerSlowingDown : MonoBehaviour
{
    public float speed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeSpeed(speed);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeSpeed(Constants.NormalSpeed);
    }
}
