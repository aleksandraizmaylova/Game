using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("���������")]
    public Transform player;
    public float interactDistance = 2f;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogueLines;

    [Header("���������")]
    public GameObject pressHint; 

    private int currentLine = 0;
    private bool isDialogueActive = false;

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
    }

    void Update()
    {
        if (player == null || dialoguePanel == null || dialogueText == null)
        {
            Debug.LogError("�� ��������� ������������ ����������!");
            return;
        }

        bool isPlayerClose = IsPlayerClose();

        if (pressHint != null)
        {
            pressHint.SetActive(isPlayerClose && !isDialogueActive);
        }

        if (isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        if (dialogueLines.Length == 0)
        {
            Debug.LogError("������ dialogueLines ������!");
            return;
        }

        dialoguePanel.SetActive(true);
        dialogueText.text = dialogueLines[0];
        currentLine = 0;
        isDialogueActive = true;
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        currentLine = 0;
    }

    bool IsPlayerClose()
    {
        return Vector3.Distance(transform.position, player.position) <= interactDistance;
    }
}