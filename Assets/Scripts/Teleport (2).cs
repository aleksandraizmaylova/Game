using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.position = new Vector3(-28, -18, 0);
        }
    }
}
