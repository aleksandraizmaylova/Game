using UnityEngine;

public class DeactivateParents : MonoBehaviour
{
    [Header("���������")]
    public float interactDistance = 3f; // ��������� ��� ��������������
    public KeyCode interactKey = KeyCode.E; // ������� ���������
    public int requiredPresses = 22; // ������ ���������� �������

    [Header("������")]
    public Transform player; // �������� ���� ������
    public GameObject Mum;   // �������� ������ "Mom"
    public GameObject Dad;   // �������� ������ "Dad"

    private int ePressCount = 0;
    private bool isInRange = false;


    void Update()
    {
        if (player == null || Mum == null || Dad == null)
        {
            Debug.LogError("�� ��������� player, mom ��� dad!");
            return;
        }

        // ��������� ��������� �� ������
        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance <= interactDistance;

        // ���� ����� � ������� � ����� E
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            ePressCount++;
            Debug.Log($"�������: {ePressCount}/{requiredPresses}");

            if (ePressCount >= requiredPresses)
            {
                DeactivateParentsObjects();
            }
        }
    }

    void DeactivateParentsObjects()
    {
        if (Mum != null) Mum.SetActive(false);
        if (Dad != null) Dad.SetActive(false);
        Debug.Log("�������� ��������������!");
        enabled = false; // ��������� ������
        Player.Instance.canMove = true;
    }

    // �����������: ������������ ������� � ���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}