using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination; // �����, ���� ���������������
    [SerializeField] private KeyCode activationKey = KeyCode.E; // ������� ���������
    [SerializeField] private float teleportCooldown = 0f; // �������� ����� �������������� (0 - ��� ��������)

    private bool playerInRange = false;
    private bool canTeleport = true;
    private float lastTeleportTime;

    private void Update()
    {
        // ��������� ����� �� �����������������
        if (playerInRange && (Input.GetKeyDown(activationKey) || activationKey is KeyCode.None) && canTeleport)
        {
            TeleportPlayer();

            // ���� ����������� �������� - ���������� �������
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