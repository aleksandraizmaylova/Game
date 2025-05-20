using UnityEngine;

public class TruckController : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 direction = Vector3.left;
    [SerializeField] private bool showDebugLogs = true;

    [Header("Состояние")]
    [SerializeField] private bool isMoving = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null && showDebugLogs)
        {
            Debug.LogWarning("Rigidbody2D не найден, будет использовано Transform движение", this);
        }
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * speed;
        }
        else
        {
            transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopTruck();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        if (showDebugLogs) Debug.Log("Грузовик начал движение", this);
    }

    public void StopTruck()
    {
        isMoving = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (showDebugLogs) Debug.Log("Грузовик остановился из-за столкновения с игроком", this);
    }
}