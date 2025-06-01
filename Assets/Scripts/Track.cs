using System;
using UnityEngine;

public class Track : MonoBehaviour
{
    public float CurrentSpeed;
    public Direction Direction;
    public GameObject Finish;

    [Header("Sound Settings")]
    public AudioClip engineSound;
    [Range(0, 1)] public float engineVolume = 0.5f;
    public float pitchMin = 0.8f;
    public float pitchMax = 1.2f;


    public bool isMoving;
    
    private Rigidbody2D rb2;
    private Vector2 start;
    private Collider2D finish;
    private AudioSource audioSource;
    private bool soundPlaying;

    void Start()
    {
        finish = Finish.GetComponent<Collider2D>();
        rb2 = GetComponent<Rigidbody2D>();
        start = rb2.position;
        CurrentSpeed += 5;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = engineSound;
        audioSource.loop = true;
        audioSource.volume = engineVolume;
    }

    public void StartMoving()
    {
        isMoving = true;
        PlayEngineSound();
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {
            StopEngineSound();
            return;
        }
        var movementVector = Direction == Direction.Left ? Vector2.left : Vector2.right;
        rb2.linearVelocity = movementVector * CurrentSpeed;
        if (soundPlaying)
        {
            float speedRatio = Mathf.Clamp01(CurrentSpeed / 20f);
            audioSource.pitch = Mathf.Lerp(pitchMin, pitchMax, speedRatio);
        }
    }

    private void PlayEngineSound()
    {
        if (engineSound != null && !soundPlaying)
        {
            audioSource.Play();
            soundPlaying = true;
        }
    }

    private void StopEngineSound()
    {
        if (soundPlaying)
        {
            audioSource.Stop();
            soundPlaying = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == finish)
            rb2.position = start;
    }
}

public enum Direction
{
    Left,
    Right
}