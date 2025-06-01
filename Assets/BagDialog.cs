using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class BagDialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button[] choiceButtons;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public GameObject pressHint;
    [SerializeField] GameObject nurse;

    [Header("Game Objects")]
    public Transform player;
    public Player playerMove;
    public GameObject bagObject;

    [Header("Settings")]
    public float interactDistance = 2f;

    private bool isDialogueActive;
    private int currentLine = 0;

    // ���������� ������
    private readonly List<string> npcLines = new List<string>
    {
        "������, ���� ������ �����...",
        "����� ����� ���������, ����� �� ��� �� ����������?",
        "���... ������������� �� �����������?",
        "������� ���� ��� ���",
        "� ��� ������������� � ������?..."
    };

    private readonly List<string[]> playerChoices = new List<string[]>
    {
        new string[] { "��������� � �����", "�� ������� �����" },
        new string[] { "���������� ���������", "�� ������� ���������" },
        new string[] { "..." },
        new string[] { "..."},
        new string[] { "..."}
    };

    private void Start()
    {
        dialoguePanel.SetActive(false);
        pressHint.SetActive(false);

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        bool isPlayerClose = Vector3.Distance(player.position, bagObject.transform.position) <= interactDistance;
        pressHint.SetActive(isPlayerClose && !isDialogueActive);

        if (isPlayerClose && Input.GetKeyDown(KeyCode.E) && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        playerMove.canMove = false;
        dialoguePanel.SetActive(true);
        UpdateDialogue();
    }

    private void UpdateDialogue()
    {
        if (!isDialogueActive) return;

        if (currentLine >= npcLines.Count)
        {
            EndDialogue();
            return;
        }

        // ���������� ������� ������� NPC
        dialogueText.text = npcLines[currentLine];

        // �������� ��� ������
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        // ���������� ��������� �������� ������
        if (currentLine < playerChoices.Count)
        {
            string[] choices = playerChoices[currentLine];
            for (int i = 0; i < choices.Length && i < choiceButtons.Length; i++)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = choices[i];

                // �����: ������� ��������� ����� �������
                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            }
        }
        else
        {
            // ���� ������ ��������
            EndDialogue();
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        // ������ ������ ��������
        switch (currentLine)
        {
            case 0: // ������ �����
                if (choiceIndex == 0) currentLine = 1; 
                else EndDialogue(); 
                break;

            case 1: // ������ �����
                if (choiceIndex == 0) currentLine = 2; 
                else EndDialogue(); 
                break;

            case 2: // ������ �����
                if (choiceIndex == 0) currentLine = 3; 
                else EndDialogue(); 
                break;

            case 3:
                if (choiceIndex == 0) currentLine = 4; 
                else EndDialogue(); 
                break;

            case 4:
                if (choiceIndex == 0)
                    EndDialogue();
                    break;


            default:
                if (currentLine + 1 < npcLines.Count)
                {
                    currentLine++;
                }
                else
                {
                    EndDialogue();
                }
                break;
        }

        UpdateDialogue();
    }

    private void EndDialogue()
    {
        nurse.SetActive(true);
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        playerMove.canMove = true;
        currentLine = 0; // ����� �������

        // �������� ��� ������ ������
        foreach (var button in choiceButtons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners(); // ������� ��� ��������
            }
        }

        enabled = false;
    }
}