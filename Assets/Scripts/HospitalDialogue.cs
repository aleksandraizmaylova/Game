using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HospitalDialogue : MonoBehaviour
{
    [Header("��������� ������")]
    public Transform player;
    public Player playerMove;
    public float interactDistance = 3f;

    [Header("�������� ����������")]
    public GameObject dialoguePanel;
    public Text nameText;
    public Text messageText;

    [Header("���������� �������")]
    public DialogueMessage[] dialogueMessages;
    public GameObject bookObject;
    public GameObject TeleportEntrance;

    public GameObject pressHint;
    public KeyCode interactKey = KeyCode.E;
    public bool fourthPhase;

    [Header("Cutscene settings")]
    public GameObject goodEnd;
    public GameObject badEnd;
    private GameObject cutscene;
    private VideoPlayer videoCutscene;

    private int currentMessageIndex = 0;
    private bool isDialogueActive = false;
    private Light2D light;

    [System.Serializable]
    public class DialogueMessage
    {
        public string speakerName;
        public string message;
    }

    void Start()
    {
        light = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        // cutscene = Player.Instance.GetComponent<Inventory>().mirror.full ? goodEnd : badEnd;
        if (fourthPhase)
        {
            if (Player.Instance.GetComponent<Inventory>().mirror.full)
            {
                cutscene = goodEnd;
                Debug.Log("загрузилась хорошая концовка");
            }
            else
            {
                cutscene = badEnd;
                Debug.Log("загрузилась плохая концовка");
            }

            videoCutscene = cutscene.GetComponent<VideoPlayer>();
            videoCutscene.loopPointReached += OnVideoFinished;
            videoCutscene.isLooping = false;
        }
        
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
            Debug.LogError("��� ��������� ��� �������!");
            return;
        }

        currentMessageIndex = 0;

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
                light.color = Color.black;
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
            Debug.LogError($"�������� ������ ���������: {currentMessageIndex}");
            return;
        }

        DialogueMessage current = dialogueMessages[currentMessageIndex];

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
            light.color = Color.white;
            nameText.color = Color.black;
            //���� �������� ��������� ��������
            
            cutscene.SetActive(true);
            MusicManager.Instance.StopMusic();
            videoCutscene.Play();
        }
    }
    
    private void OnVideoFinished(VideoPlayer vp)
    {
        cutscene.SetActive(false);
        SceneManager.LoadScene("MainMenuScene");
    }
}