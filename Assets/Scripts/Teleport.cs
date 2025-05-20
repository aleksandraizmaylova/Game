// using UnityEngine;
//
// public class Teleporter : MonoBehaviour
// {
//     [SerializeField] private Transform destination; // �����, ���� ���������������
//     [SerializeField] private KeyCode activationKey = KeyCode.E; // ������� ���������
//     [SerializeField] private float teleportCooldown = 0f; // �������� ����� �������������� (0 - ��� ��������)
//
//     private bool playerInRange = false;
//     private bool canTeleport = true;
//     private float lastTeleportTime;
//
//     private void Update()
//     {
//         // ��������� ����� �� �����������������
//         if (playerInRange && (Input.GetKeyDown(activationKey) || activationKey is KeyCode.None) && canTeleport)
//         {
//             TeleportPlayer();
//
//             // ���� ����������� �������� - ���������� �������
//             if (teleportCooldown > 0)
//             {
//                 canTeleport = false;
//                 lastTeleportTime = Time.time;
//                 Invoke(nameof(ResetTeleportCooldown), teleportCooldown);
//             }
//         }
//     }
//
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerInRange = true;
//             Debug.Log("Press E to teleport"); // reivew: если что, пользователь этого не увидит
//         }
//     }
//
//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerInRange = false;
//         }
//     }
//
//     private void TeleportPlayer()
//     {
//         // review: var
//         GameObject player = GameObject.FindGameObjectWithTag("Player");
//         if (player != null && destination != null)
//         {
//             player.transform.position = destination.position;
//             Debug.Log($"Teleported to {destination.name} at {Time.time}");
//         }
//     }
//
//     private void ResetTeleportCooldown()
//     {
//         canTeleport = true;
//     }
//
//     private void OnDrawGizmos()
//     {
//         if (destination != null)
//         {
//             Gizmos.color = Color.blue;
//             Gizmos.DrawLine(transform.position, destination.position);
//             Gizmos.DrawWireSphere(destination.position, 0.5f);
//         }
//     }
// }


using UnityEngine;
using Unity.Cinemachine;

public class Teleporter : MonoBehaviour
{
    [Header("Teleport Settings")]
    [SerializeField] private Transform destination;
    [SerializeField] private CinemachineCamera targetVCam;

    [SerializeField] private KeyCode activationKey = KeyCode.E;
    [SerializeField] private float teleportCooldown = 0f;

    private bool playerInRange = false;
    private bool canTeleport = true;

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(activationKey) || activationKey == KeyCode.None) && canTeleport)
        {
            TeleportPlayer();

            if (teleportCooldown > 0)
            {
                canTeleport = false;
                Invoke(nameof(ResetTeleportCooldown), teleportCooldown);
            }
        }
    }

    private void TeleportPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || destination == null || targetVCam == null)
        {
            Debug.LogWarning("Teleport failed: missing data.");
            return;
        }

        // Переместим игрока
        player.transform.position = destination.position;

        // Отключим все другие камеры
        foreach (var vcam in FindObjectsOfType<CinemachineCamera>())
        {
            vcam.enabled = false;
        }

        // Включим нужную камеру
        targetVCam.enabled = true;

        Debug.Log($"Teleported and switched to camera {targetVCam.name}");
    }

    private void ResetTeleportCooldown() => canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
