using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private Transform destination;
    [SerializeField] private float teleportCooldown = 0f;
    [SerializeField] private bool showDebugLogs = true;
    [SerializeField] private string requiredKeyName = "Key";

    [Header("Ссылка на грузовик")]
    [SerializeField] private TruckController truckController;

    private bool canTeleport = true;
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        if (truckController == null && showDebugLogs)
        {
            Debug.LogWarning("TruckController не назначен в инспекторе!", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !canTeleport) return;

        if (HasRequiredKey())
        {
            TeleportPlayer(other.transform);
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

    private void TeleportPlayer(Transform playerTransform)
    {
        if (destination == null)
        {
            Debug.LogError("Цель телепортации не назначена!", this);
            return;
        }

        playerTransform.position = destination.position;
        if (showDebugLogs) Debug.Log($"Игрок телепортирован к {destination.name}", this);

        if (teleportCooldown > 0)
        {
            canTeleport = false;
            Invoke(nameof(ResetTeleport), teleportCooldown);
        }
    }

    private void ActivateTruck()
    {
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