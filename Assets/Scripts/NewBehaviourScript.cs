using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    public float speed = 1;

    [SerializeField] private Camera camera;
    private Transform transform;
    private Rigidbody2D rigidbody2D;
    
    void Start()
    {
        transform = GetComponent<Transform>();
        transform.position = new Vector3();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var vector = new Vector3();
        if (Input.GetKey(KeyCode.A))
        {
            vector -= new Vector3(0.005f, 0);
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        if (Input.GetKey(KeyCode.W))
        {
            vector += new Vector3(0, 0.005f);
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (Input.GetKey(KeyCode.S))
        {
            vector -= new Vector3(0, 0.005f);
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        if (Input.GetKey(KeyCode.D))
        {
            vector += new Vector3(0.005f, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.position += vector;
        rigidbody2D.linearVelocity = vector * speed;
        camera.transform.position =
            new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        if (transform.position.y < -5)
            transform.position = new Vector3();
    }
}