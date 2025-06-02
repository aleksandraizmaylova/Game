using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HospitalDialogue : MonoBehaviour
{
    [Header("Настройки игрока")]
    public Transform player;
    public Player playerMove;
    public float interactDistance = 3f;

    [Header("Элементы интерфейса")]
    public GameObject dialoguePanel;
    public Text nameText;
    public Text messageText;

    [Header("Содержание диалога")]
    public DialogueMessage[] dialogueMessages;
    public GameObject bookObject;
    public GameObject TeleportEntrance;

    public GameObject pressHint;
    public KeyCode interactKey = KeyCode.E;
    public bool fourthPhase;

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

        if ((!isPlayerInRange && isDialogueActive) ||
            (isDialogueActive && currentMessageIndex >= dialogueMessages.Length))
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        if (dialogueMessages == null || dialogueMessages.Length == 0)
        {
            Debug.LogError("Нет сообщений для диалога!");
            return;
        }

        currentMessageIndex = 0;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Не назначена панель диалога!");
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

            if (currentMessageIndex == 8 && bookObject != null)
            {
                bookObject.SetActive(true);
            }

            if (currentMessageIndex == 30 && TeleportEntrance != null)
            {
                TeleportEntrance.SetActive(true);
            }

            if (currentMessageIndex == 6 && TeleportEntrance != null)
            {
                TeleportEntrance.SetActive(true);
            }

            if (fourthPhase && currentMessageIndex == 10)
            {
                GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>().enabled = false;
                nameText.color = Color.gray;
            }
        }
        else
        {
            EndDialogue();
        }
    }

    void UpdateDialogueDisplay()
    {
        if (currentMessageIndex < 0 || currentMessageIndex >= dialogueMessages.Length)
        {
            Debug.LogError($"Неверный индекс сообщения: {currentMessageIndex}");
            return;
        }

        DialogueMessage current = dialogueMessages[currentMessageIndex];

        if (nameText != null)
        {
            nameText.text = current.speakerName + ":";
        }
        else
        {
            Debug.LogError("Не назначено поле для имени!");
        }

        if (messageText != null)
        {
            messageText.text = current.message;
        }
        else
        {
            Debug.LogError("Не назначено поле для сообщения!");
        }
    }

    void EndDialogue()
    {
        if (playerMove != null)
        {
            playerMove.canMove = true;
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        nameText.text = "";
        isDialogueActive = false;
        currentMessageIndex = 0;
        if (fourthPhase)
        {
            GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>().enabled = true;
            nameText.color = Color.black;
            //сюда запихать включение катсцены
        }
    }
}