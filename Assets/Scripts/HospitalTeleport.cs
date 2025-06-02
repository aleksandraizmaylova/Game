using Unity.Cinemachine;
using UnityEngine;

public class HospitalTeleporter : MonoBehaviour
{
    [Header("�������� ���������")]
    [SerializeField] private Transform destination; // ����� ����������
    [SerializeField] private KeyCode activationKey = KeyCode.E; // ������� ���������
    [SerializeField] private int requiredPresses = 30; // ������� ��� ����� ������
    [Space]
    [SerializeField] private float teleportCooldown = 0f; // ������� ������������
    [SerializeField] private bool canTeleport = true; // ����� �� �����������������

    [Header("��������� ������")]
    [SerializeField] private CinemachineCamera targetVCam; // ������� ����������� ������

    private bool playerInRange = false;
    private int pressCount = 0; // ������� �������

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(activationKey))
        {
            pressCount++; // ����������� �������

            if (pressCount >= requiredPresses && canTeleport)
            {
                TeleportPlayer(); // �������������, ���� ������� ����������
                pressCount = 0; // ���������� �������
            }
            else
            {
                Debug.Log($"������ {pressCount}/{requiredPresses} ���");
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
            Debug.LogWarning("������������ ����������: ����������� �����, ����� ���������� ��� ������!");
            return;
        }

        // ���������� ������
        player.transform.position = destination.position;
        Player.Instance.canMove = true;
        Player.Instance.ChangeSpeed(Constants.NormalSpeed);

        // ��������� ��� ������ ������ Cinemachine
        foreach (var vcam in FindObjectsOfType<CinemachineCamera>())
        {
            vcam.enabled = false;
        }

        // �������� ������� ������
        targetVCam.enabled = true;
        Debug.Log($"����� ��������������! ������: {targetVCam.name}");

        // ���������� ������� (���� �� �����)
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