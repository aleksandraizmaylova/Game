using System;
using UnityEngine;

public class Track : MonoBehaviour
{
    public float CurrentSpeed;
    public Direction Direction;
    public GameObject Finish;
    
    private Rigidbody2D rb2;
    private Vector2 start;
    private Collider2D finish;
    
    void Start()
    {
        finish = Finish.GetComponent<Collider2D>();
        rb2 = GetComponent<Rigidbody2D>();
        start = rb2.position;
    }

    private void FixedUpdate()
    {
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