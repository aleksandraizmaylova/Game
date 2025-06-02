using System;
using UnityEngine;

public class PlayerSlowingDown : MonoBehaviour
{
    public float speed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (speed == 0)
                Player.Instance.canMove = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeSpeed(speed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (speed == 0)
                Player.Instance.canMove = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeSpeed(Constants.NormalSpeed);
        }
    }
}
