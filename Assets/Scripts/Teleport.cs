using System;
using Unity.Cinemachine;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private Transform destination;
    [SerializeField] private float teleportCooldown;
    [SerializeField] private KeyCode activationKey = KeyCode.E;
    [Space]
    [SerializeField] private bool isConditional;
    [SerializeField] private string requiredKeyName = "Key";
    [SerializeField] public GameObject nurse;

    [Header("Teleport Settings")]
    [SerializeField] private CinemachineCamera targetVCam;

    [SerializeField] private bool allowMoving = true;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip teleportSound;

    private bool playerInRange = false;
    private bool canTeleport = true;
    private Inventory playerInventory;
    private AudioSource audioSource;

    private void Start()
    {
        if (!TryGetComponent<AudioSource>(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (isConditional)
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        nurse.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange 
            && (Input.GetKeyDown(activationKey) || activationKey == KeyCode.None)
            && (!isConditional || HasRequiredKey()))
            TeleportPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
            playerInRange = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private bool HasRequiredKey()
    {
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

        PlayTeleportSound();

        // Переместим игрока
        player.transform.position = destination.position;
        if (!allowMoving)
            player.GetComponent<Player>().canMove = false;

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

    private void PlayTeleportSound()
    {
        if (teleportSound != null)
        {
            AudioSource.PlayClipAtPoint(teleportSound, transform.position);
        }
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