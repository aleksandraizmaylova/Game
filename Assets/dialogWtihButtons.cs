using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Text npcText;
    public Button[] choiceButtons;
    public Transform player;
    public GameObject fragment;
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
    private bool IsEnd = false;

    private List<string> npcLines = new List<string> {
        "Ну Здравствуй",
        "...",
        "я дам только один шанс выбраться отсюда. Слушай внимательно.",
        "что появилось первым, курица или яйцо?",
        "Если заменить все части корабля, тот же ли это корабль?",
        "Что такое «я» — тело, разум или душа?",
        "Возможен ли вечный двигатель?"
    };

    private List<string[]> playerChoices = new List<string[]> {
        new string[] { "ты кто?", "мы знакомы?" },
        new string[] { "..." },
        new string[] { "..." },
        new string[] { "Курица", "Яйцо", "Что?"},
        new string[] { "Нет", "Да", "Чего???"},
        new string[] { "Тело + разум", "Только душа", "Ты это мне?" },
        new string[] { "A?", "Нет", "Ха! конечно!"}


    };

    private List<int[]> choisesValue = new List<int[]>
    {
        new int[] { 0, 0 },
        new int[] { 0 },
        new int[] { 0 },
        new int[] { -1, 1, -1 },
        new int[] { 1, -1, -1 },
        new int[] { 1, -1, -1 },
        new int[] { -1, 1, -1 }


    };

    private int currentLine = 0;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (pressHint != null)
            pressHint.SetActive(false);

        if(!isDialogueActive) 
        { 
           foreach(var button in choiceButtons)
               button.gameObject.SetActive(false);
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
                StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        playerMove.canMove = false;
        dialogueText.text = npcLines[currentLine];
        for (var i = 0; i < choiceButtons.Length; i++)
        {
            if (i < playerChoices[currentLine].Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = playerChoices[currentLine][i];
                var choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => 
                { 
                    OnChoiceSelected(choiceIndex);
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        value += choisesValue[currentLine][choiceIndex];
        Debug.Log("Игрок выбрал: " + playerChoices[currentLine][choiceIndex]);
        currentLine++;
        if (currentLine < npcLines.Count)
            StartDialogue();
        else
        {
            foreach (var button in choiceButtons)
                button.gameObject.SetActive(false);
            if (value == 4)
            {
                dialogueText.text = "Я удивлен твоими познаниями! хорошо, этот осколок твой.";
                choiceButtons[0].gameObject.SetActive(true);
                choiceButtons[0].GetComponentInChildren<Text>().text = "Откуда я все это знаю??";
                choiceButtons[0].onClick.RemoveAllListeners();
                choiceButtons[0].onClick.AddListener(() => { choiceButtons[0].gameObject.SetActive(false); dialoguePanel.SetActive(false); });
                fragment.SetActive(true);
            }
            else
            {

                dialogueText.text = "Я в тебе разочарован, убирайся ни с чем.";
                choiceButtons[0].gameObject.SetActive(true);
                choiceButtons[0].GetComponentInChildren<Text>().text = "...";
                choiceButtons[0].onClick.RemoveAllListeners();
                choiceButtons[0].onClick.AddListener(() => { choiceButtons[0].gameObject.SetActive(false); dialoguePanel.SetActive(false); });
                player.position = new Vector3(-28, -18, 0);
            }
            playerMove.canMove = true;
            IsEnd = true;
        }
    }
}