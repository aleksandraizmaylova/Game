using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Transform player;
    public GameObject keyObj;
    public float interactDistance = 2f;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogueLines;
    private int[] stopLine;
    private bool[] actions;
    public int action = 0;
    private PickUp pkup;

    public GameObject pressHint; 

    public int currentLine = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        pkup = keyObj.GetComponent<PickUp>();
        stopLine = new int[] { 2 };
        actions = new[] { false };
        
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
        if(pkup.isPicked)
        {
            actions[0] = true;
        }
        bool isPlayerClose = IsPlayerClose();

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
            else
            {
                NextLine();
            }
        }

        if (!isPlayerClose && isDialogueActive)
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];
        isDialogueActive = true;
    }

    void NextLine()
    {
        if(currentLine < dialogueLines.Length)
        {
            if(stopLine.Contains(currentLine) && actions[action] == true)
            {
                if (currentLine < dialogueLines.Length)
                {
                    currentLine++;
                    dialogueText.text = dialogueLines[currentLine];
                }
                if(action < stopLine.Length - 1 && action < actions.Length - 1)
                {
                    action++;
                }
            }
            else
            {
                if(currentLine < stopLine[action])
                {
                    currentLine++;
                    dialogueText.text = dialogueLines[currentLine];
                }
                else
                {
                    EndDialogue();
                }
            }
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
        if(!actions[action])
        {
            currentLine = stopLine[action];
        }
    }

    bool IsPlayerClose()
    {
        return Vector3.Distance(transform.position, player.position) <= interactDistance;
    }
}