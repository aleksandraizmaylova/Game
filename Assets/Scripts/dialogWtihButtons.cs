using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Button[] choiceButtons;
    public Transform player;
    public GameObject fragment;
    public Player playerMove;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public GameObject pressHint;
    public GameObject teleporter;
    private Animator animator;
    private const float interactDistance = 2f;
    private bool isDialogueActive;
    private int value;
    private int currentLine;

    private List<string> npcLines = new()
    {
        "Ну Здравствуй",
        "...",
        "я дам только один шанс выбраться отсюда. Слушай внимательно.",
        "что появилось первым, курица или яйцо?",
        "Если заменить все части корабля, тот же ли это корабль?",
        "Что такое «я» — тело, разум или душа?",
        "Возможен ли вечный двигатель?"
    };

    private List<string[]> playerChoices = new()
    {
        new[] { "ты кто?", "мы знакомы?" },
        new[] { "..." },
        new[] { "..." },
        new[] { "Курица", "Яйцо", "Что?"},
        new[] { "Нет", "Да", "Чего???"},
        new[] { "Тело + разум", "Только душа", "Ты это мне?" },
        new[] { "A?", "Нет", "Ха! конечно!"}
    };

    private List<int[]> choisesValue = new()
    {
        new[] { 0, 0 },
        new[] { 0 },
        new[] { 0 },
        new[] { -1, 1, -1 },
        new[] { 1, -1, -1 },
        new[] { 1, -1, -1 },
        new[] { -1, 1, -1 }
    };

    void Start()
    {
        animator = GetComponent<Animator>();
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
        return Vector2.Distance(transform.position, player.position) <= interactDistance;
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
        animator.SetTrigger("WolfSaid");
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
        {
            animator.ResetTrigger("WolfSaid");
            StartDialogue();
        }
        else
        {
            foreach (var button in choiceButtons)
                button.gameObject.SetActive(false);
            if (value == 4)
            {
                animator.SetTrigger("WolfSaid");
                dialogueText.text = "Я удивлен твоими познаниями! хорошо, этот осколок твой.";
                choiceButtons[0].gameObject.SetActive(true);
                choiceButtons[0].GetComponentInChildren<Text>().text = "Откуда я все это знаю??";
                choiceButtons[0].onClick.RemoveAllListeners();
                choiceButtons[0].onClick.AddListener(() => { 
                        choiceButtons[0].gameObject.SetActive(false); 
                        dialoguePanel.SetActive(false); 
                        fragment.SetActive(true);
                        playerMove.canMove = true;});
            }
            else
            {
                animator.SetTrigger("WolfSaid");
                dialogueText.text = "Я в тебе разочарован, убирайся ни с чем.";
                choiceButtons[0].gameObject.SetActive(true);
                choiceButtons[0].GetComponentInChildren<Text>().text = "...";
                choiceButtons[0].onClick.RemoveAllListeners();
                choiceButtons[0].onClick.AddListener(() => { choiceButtons[0].gameObject.SetActive(false); dialoguePanel.SetActive(false); teleporter.SetActive(true);});
            }
        }
    }
}