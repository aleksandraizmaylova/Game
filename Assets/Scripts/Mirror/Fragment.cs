using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Video;

public class Fragment : MonoBehaviour
{
    public GameObject cutscene;
    private Mirror Mirror;
    public int fragmentNumber;
    private VideoPlayer videoCutscene;
    [Header("TP Settings")]
    public GameObject destination;
    public CinemachineCamera targetCam;
    public CinemachineCamera currentCam;
    public bool AllowMoving = true;

    private void Start()
    {
        Mirror = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().mirror;
        videoCutscene = cutscene.GetComponent<VideoPlayer>();
        videoCutscene.loopPointReached += OnVideoFinished;
        videoCutscene.isLooping = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cutscene.SetActive(true);
            videoCutscene.Play();
            Mirror.ActivateFragment(fragmentNumber);
            if (destination != null)
                TeleportPlayer();
            Destroy(gameObject);
        }
    }
    private void TeleportPlayer()
    {
        Player.Instance.transform.position = destination.transform.position;
        if (!AllowMoving)
            Player.Instance.canMove = false;
        currentCam.enabled = false;
        targetCam.enabled = true;
    }
    private void OnVideoFinished(VideoPlayer vp)
    {
        cutscene.SetActive(false);
    }
}