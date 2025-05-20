using UnityEngine;

public class TruckController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 direction = Vector3.left;
    [SerializeField] private bool startMoving = false;

    private void Update()
    {
        if (startMoving)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        startMoving = true;
    }
}