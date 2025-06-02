using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    [Header("Phone Settings")]
    public Animator animator;
    public int phoneNumber = -1;
    public GameObject newCanvas;
    public GameObject oldCanvas;
    public GameObject fragment;
    [Space]
    [Header("Dialog Settings")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    [Space]
    [Header("Teleport Settings")]
    public CinemachineCamera closeUpCamera;
    public CinemachineCamera longShotCamera;
    public GameObject phone;
    public CinemachineCamera hospitalCamera;
    public GameObject hospitalTeleport;

    [Header("Sound Settings")]
    public AudioClip buttonPressSound;
    public AudioClip ringtoneSound;

    private int result;
    private Player player;
    private Vector2 position;
    private AudioSource audioSource;

    private void Start()
    {
        player = Player.Instance;
        position= phone.transform.position + Vector3.down;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.canMove = false;
            oldCanvas.SetActive(false);
            newCanvas.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //player.canMove = true;
            newCanvas.SetActive(false);
            oldCanvas.SetActive(true);
        }
    }

    public void OnAnimationEnding()
    {
        dialoguePanel.SetActive(false);
        if (result == phoneNumber)
        {
            fragment.SetActive(true);
            phone.SetActive(false);
            result = -1;
            closeUpCamera.enabled = false;
            longShotCamera.enabled = true;
            player.transform.position = position;
        }
        if (result > phoneNumber)
        {
            phone.SetActive(false);
            result = -1;
            closeUpCamera.enabled = false;
            hospitalCamera.enabled = true;
            player.transform.position = hospitalTeleport.transform.position;
            player.canMove = false;
        }
    }

    public void Push(int i)
    {
        PlayButtonSound();
        result = result * 10 + i;
        Debug.Log($"result: {result}");
        
        if (result >= phoneNumber)
        {
            animator.SetTrigger($"{i}");
            dialoguePanel.SetActive(true);
            dialogueText.text = "Гудки...";
            PlayRingtone();
            return;
        }
        
        animator.SetTrigger($"{i}");
        dialoguePanel.SetActive(true);
        dialogueText.text = $"{i}...";
    }
    private void PlayButtonSound()
    {
        if (buttonPressSound != null)
        {
            audioSource.PlayOneShot(buttonPressSound);
        }
    }

    private void PlayRingtone()
    {
        if (ringtoneSound != null)
        {
            audioSource.PlayOneShot(ringtoneSound);
        }
    }
}
