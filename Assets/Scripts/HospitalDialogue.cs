using UnityEngine;
using UnityEngine.UI;

public class HospitalDialogue : MonoBehaviour
{
    [Header("��������� ������")]
    public Transform player;
    public float interactDistance = 3f;

    [Header("�������� ����������")]
    public GameObject dialoguePanel;
    public Text nameText;
    public Text messageText;

    [Header("���������� �������")]
    public DialogueMessage[] dialogueMessages;
    public GameObject bookObject; // ������ �� ��� ������������ ����� �� �����
    public GameObject TeleportEntrance;

    public GameObject pressHint;
    public KeyCode interactKey = KeyCode.E;

    private int currentMessageIndex = 0;
    private bool isDialogueActive = false;

    [System.Serializable]
    public class DialogueMessage
    {
        public string speakerName;
        public string message;
    }

    void Start()
    {
        // �������� ����� � ������ ����
        if (bookObject != null)
        {
            bookObject.SetActive(false);
        }

        if (TeleportEntrance != null)
        {
            TeleportEntrance.SetActive(false);
        }
    }

    void Update()
    {
        // �������� �� null ��� player
        if (player == null) 
            return;

        bool isPlayerInRange = Vector3.Distance(transform.position, player.position) <= interactDistance;

        if (pressHint != null)
        {
            pressHint.SetActive(isPlayerInRange && !isDialogueActive);
        }

        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                ShowNextMessage();
            }
        }

        if (!isPlayerInRange && isDialogueActive)
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        // �������� ������� ���������
        if (dialogueMessages == null || dialogueMessages.Length == 0)
        {
            Debug.LogError("��� ��������� ��� �������!");
            return;
        }

        currentMessageIndex = 0;

        // �������� � ��������� ������
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        else
        {
            Debug.LogError("�� ��������� ������ �������!");
            return;
        }

        UpdateDialogueDisplay();
        isDialogueActive = true;
    }

    void ShowNextMessage()
    {
        currentMessageIndex++;

        if (currentMessageIndex < dialogueMessages.Length)
        {
            UpdateDialogueDisplay();

            // ���������� ����� ����� 7-� ������� (������ 6, ��� ��� ������ � 0)
            if (currentMessageIndex == 8 && bookObject != null)
            {
                bookObject.SetActive(true);
            }

            if (currentMessageIndex == 30 && TeleportEntrance != null)
            {
                TeleportEntrance.SetActive(true);
            }
        }
        else
        {
            EndDialogue();
        }
    }

    void UpdateDialogueDisplay()
    {
        // �������� �� ����� �� ������� �������
        if (currentMessageIndex < 0 || currentMessageIndex >= dialogueMessages.Length)
        {
            Debug.LogError($"�������� ������ ���������: {currentMessageIndex}");
            return;
        }

        DialogueMessage current = dialogueMessages[currentMessageIndex];

        // �������� � ���������� ��������� �����
        if (nameText != null)
        {
            nameText.text = current.speakerName + ":";
        }
        else
        {
            Debug.LogError("�� ��������� ���� ��� �����!");
        }

        if (messageText != null)
        {
            messageText.text = current.message;
        }
        else
        {
            Debug.LogError("�� ��������� ���� ��� ���������!");
        }
    }

    void EndDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        isDialogueActive = false;
        currentMessageIndex = 0;
    }
}