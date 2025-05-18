using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Text npcText;  // ����� ������� NPC
    public Button[] choiceButtons;  // ������ ������ ������



    public Transform player;
    public Player playerMove;
    public GameObject keyObj;
    public float interactDistance = 2f;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogueLines;
    public int action = 0;
    public GameObject pressHint;
    private bool isDialogueActive = false;
    private int value = 0;

    // ������ ������ ������� (����� �������� �� ����� ��� JSON)
    private List<string> npcLines = new List<string> {
        "�� ����������",
        "...",
        "� ��� ������ ���� ���� ��������� ������. ������ �����������.",
        "��� ��������� ������, ������ ��� ����?",
        ""
    };

    private List<string[]> playerChoices = new List<string[]> {
        new string[] { "�� ���?", "�� �������?" },
        new string[] { "..." },
        new string[] { "..." },
        new string[] { "������", "����", "���?"}
    };

    private List<int[]> choisesValue = new List<int[]>
    {
        new int[] { 0, 0 },
        new int[] { 0 },
        new int[] { 0 },
        new int[] { -1, 1, -1}
    };

    private int currentLine = 0;

    void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (pressHint != null)
        {
            pressHint.SetActive(false);
        }

        if(!isDialogueActive) 
        { 
           foreach(var button in choiceButtons)
           {
               button.gameObject.SetActive(false);
           } 
        }
    }

    bool IsPlayerClose()
    {
        return Vector3.Distance(transform.position, player.position) <= interactDistance;
    }

    private void Update()
    {
        var isPlayerClose = IsPlayerClose();
        if (pressHint != null)
        {
            pressHint.SetActive(isPlayerClose && !isDialogueActive);
            var textPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.5f + Vector3.right);
            pressHint.transform.position = textPosition;
        }

        if (isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        playerMove.canMove = false;
        dialogueText.text = npcLines[currentLine];

        // ���������� ������ ���������� ������
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < playerChoices[currentLine].Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = playerChoices[currentLine][i];

                // �����: ��������� ������ ������ ����� ���������
                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        Debug.Log("����� ������: " + playerChoices[currentLine][choiceIndex]);
        value += choisesValue[currentLine][choiceIndex];

        // ������� � ��������� ������� (��� �������� �������)
        currentLine++;
        if (currentLine < npcLines.Count)
        {
            StartDialogue();
        }
        else
        {
            if (value < 1)
            {
                npcLines.Add("�� ���������, ������.....");
                playerChoices.Add(new string[] {"..."});
                StartDialogue();
            }
            foreach (var button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }
            dialoguePanel.SetActive(false);
            playerMove.canMove = true;
        }
    }
}