using UnityEngine;
using UnityEngine.UI;

public class HospitalDialogue : MonoBehaviour
{
    [Header("Настройки игрока")]
    public Transform player;
    public float interactDistance = 3f;

    [Header("Элементы интерфейса")]
    public GameObject dialoguePanel;
    public Text nameText;
    public Text messageText;

    [Header("Содержание диалога")]
    public DialogueMessage[] dialogueMessages;
    public GameObject bookObject; // Ссылка на уже существующую книгу на сцене
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
        // Скрываем книгу в начале игры
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
        // Проверка на null для player
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
        // Проверка наличия сообщений
        if (dialogueMessages == null || dialogueMessages.Length == 0)
        {
            Debug.LogError("Нет сообщений для диалога!");
            return;
        }

        currentMessageIndex = 0;

        // Проверка и активация панели
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

            // Активируем книгу после 7-й реплики (индекс 6, так как отсчет с 0)
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
        // Проверка на выход за границы массива
        if (currentMessageIndex < 0 || currentMessageIndex >= dialogueMessages.Length)
        {
            Debug.LogError($"Неверный индекс сообщения: {currentMessageIndex}");
            return;
        }

        DialogueMessage current = dialogueMessages[currentMessageIndex];

        // Проверка и обновление текстовых полей
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
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        isDialogueActive = false;
        currentMessageIndex = 0;
    }
}