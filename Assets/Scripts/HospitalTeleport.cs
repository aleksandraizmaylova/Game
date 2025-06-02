using Unity.Cinemachine;
using UnityEngine;

public class HospitalTeleporter : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private Transform destination; // Точка назначения
    [SerializeField] private KeyCode activationKey = KeyCode.E; // Клавиша активации
    [SerializeField] private int requiredPresses = 30; // Сколько раз нужно нажать
    [Space]
    [SerializeField] private float teleportCooldown = 0f; // Кулдаун телепортации
    [SerializeField] private bool canTeleport = true; // Можно ли телепортироваться

    [Header("Настройки камеры")]
    [SerializeField] private CinemachineCamera targetVCam; // Целевая виртуальная камера

    private bool playerInRange = false;
    private int pressCount = 0; // Счётчик нажатий

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(activationKey))
        {
            pressCount++; // Увеличиваем счётчик

            if (pressCount >= requiredPresses && canTeleport)
            {
                TeleportPlayer(); // Телепортируем, если нажатий достаточно
                pressCount = 0; // Сбрасываем счётчик
            }
            else
            {
                Debug.Log($"Нажато {pressCount}/{requiredPresses} раз");
            }
        }
    }

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

    private void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || destination == null || targetVCam == null)
        {
            Debug.LogWarning("Телепортация невозможна: отсутствует игрок, точка назначения или камера!");
            return;
        }

        // Перемещаем игрока
        player.transform.position = destination.position;
        Player.Instance.canMove = true;
        Player.Instance.ChangeSpeed(Constants.NormalSpeed);

        // Отключаем все другие камеры Cinemachine
        foreach (var vcam in FindObjectsOfType<CinemachineCamera>())
        {
            vcam.enabled = false;
        }

        // Включаем целевую камеру
        targetVCam.enabled = true;
        Debug.Log($"Игрок телепортирован! Камера: {targetVCam.name}");

        // Активируем кулдаун (если он задан)
        if (teleportCooldown > 0)
        {
            canTeleport = false;
            Invoke(nameof(ResetTeleport), teleportCooldown);
        }
    }

    private void ResetTeleport()
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