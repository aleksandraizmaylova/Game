using Unity.Cinemachine;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private Transform destination;
    [SerializeField] private float teleportCooldown;
    [SerializeField] private bool showDebugLogs = true;
    [SerializeField] private KeyCode activationKey = KeyCode.E;
    [SerializeField] private string requiredKeyName = "Key";

    [Header("Ссылка на грузовик")]
    [SerializeField] private bool truckNeeded;
    [SerializeField] private TruckController truckController;

    [Header("Teleport Settings")]
    [SerializeField] private CinemachineCamera targetVCam;


    private bool playerInRange = false;
    private bool canTeleport = true;
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        if (truckController == null && showDebugLogs && truckNeeded)
        {
            Debug.LogWarning("TruckController не назначен в инспекторе!", this);
        }
    }

    private void ResetTeleportCooldown() => canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !canTeleport) return;

        if (HasRequiredKey())
        {
            TeleportPlayer();
            ActivateTruck();
        }
        else if (showDebugLogs)
        {
            Debug.Log($"Для телепортации нужен ключ: {requiredKeyName}", this);
        }
        
    }

    private bool HasRequiredKey()
    {
        if (string.IsNullOrEmpty(requiredKeyName)) return true;
        if (playerInventory == null) return false;

        var backpack = playerInventory.openedBP?.GetComponent<BackPack>();
        if (backpack == null) return false;

        foreach (var slot in backpack.Slots)
        {
            if (slot.transform.childCount > 0)
            {
                Transform item = slot.transform.GetChild(0);
                if (item.name.Contains(requiredKeyName))
                {
                    if (showDebugLogs) Debug.Log($"Найден ключ: {item.name}", this);
                    return true;
                }
            }
        }
        return false;
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

        if (teleportCooldown > 0)
        {
            canTeleport = false;
            Invoke(nameof(ResetTeleport), teleportCooldown);
        }
    }

    private void ActivateTruck()
    {
        if (!truckNeeded)
            return;
        if (truckController == null)
        {
            Debug.LogError("Не могу активировать грузовик - контроллер не назначен!", this);
            return;
        }

        truckController.StartMoving();
        if (showDebugLogs) Debug.Log("Грузовик получил команду двигаться", this);
    }

    private void ResetTeleport() => canTeleport = true;

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