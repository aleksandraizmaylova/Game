using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float teleportCooldown = 0f;
    [SerializeField] private bool showDebugLogs = true;
    [SerializeField] private TruckController truckController; // Ссылка на скрипт грузовика

    private bool canTeleport = true;
    private float lastTeleportTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            TeleportPlayer(other.transform);

            // Активируем грузовик после телепортации
            if (truckController != null)
            {
                truckController.StartMoving();
            }
            else if (showDebugLogs)
            {
                Debug.LogWarning("TruckController reference not set in Teleporter script!");
            }
        }
    }

    private void TeleportPlayer(Transform playerTransform)
    {
        if (destination == null)
        {
            Debug.LogError("Destination not set in the Teleporter script!");
            return;
        }

        playerTransform.position = destination.position;

        if (showDebugLogs)
        {
            Debug.Log($"Player teleported to {destination.name} at {Time.time}");
        }

        if (teleportCooldown > 0)
        {
            canTeleport = false;
            lastTeleportTime = Time.time;
            Invoke(nameof(ResetTeleportCooldown), teleportCooldown);
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