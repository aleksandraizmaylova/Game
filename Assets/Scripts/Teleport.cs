using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination; // Точка, куда телепортировать
    [SerializeField] private KeyCode activationKey = KeyCode.E; // Клавиша активации
    [SerializeField] private float teleportCooldown = 0f; // Задержка между телепортациями (0 - без задержки)

    private bool playerInRange = false;
    private bool canTeleport = true;
    private float lastTeleportTime;

    private void Update()
    {
        // Проверяем можно ли телепортироваться
        if (playerInRange && Input.GetKeyDown(activationKey) && canTeleport)
        {
            TeleportPlayer();

            // Если установлена задержка - активируем кулдаун
            if (teleportCooldown > 0)
            {
                canTeleport = false;
                lastTeleportTime = Time.time;
                Invoke(nameof(ResetTeleportCooldown), teleportCooldown);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to teleport");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && destination != null)
        {
            player.transform.position = destination.position;
            Debug.Log($"Teleported to {destination.name} at {Time.time}");
        }
    }

    private void ResetTeleportCooldown()
    {
        canTeleport = true;
    }

    private void OnDrawGizmos()
    {
        if (destination != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, destination.position);
            Gizmos.DrawWireSphere(destination.position, 0.5f);
        }
    }
}