using System;
using UnityEngine;

public class Track : MonoBehaviour
{
    public float CurrentSpeed;
    public Direction Direction;
    public GameObject Finish;
    
    public bool isMoving;
    
    private Rigidbody2D rb2;
    private Vector2 start;
    private Collider2D finish;
    
    void Start()
    {
        finish = Finish.GetComponent<Collider2D>();
        rb2 = GetComponent<Rigidbody2D>();
        start = rb2.position;
        CurrentSpeed += 5;
    }

    public void StartMoving() => isMoving = true;

    private void FixedUpdate()
    {
        if (!isMoving) 
            return;
        var movementVector = Direction == Direction.Left ? Vector2.left : Vector2.right;
        rb2.linearVelocity = movementVector * CurrentSpeed;
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