using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rb2;

    private float currentSpeed;

    private float minMovingSpeed = 0.1f;

    private bool isRunningW = false;
    private bool isRunningA = false;
    private bool isRunningS = false;
    private bool isRunningD = false;

    private void Awake()
    {
        Instance = this;

        rb2 = GetComponent<Rigidbody2D>();
        rb2.freezeRotation = true;
        currentSpeed = Constants.NormalSpeed;
    }

    private void Update() => ChangeSpeed();

    private void FixedUpdate() => HandleMovement();

    private void ChangeSpeed()
    {
        if (GameInput.Instance.IsSneaking())
            currentSpeed = Constants.SneakySpeed;
        else if (GameInput.Instance.IsSprinting())
            currentSpeed = Constants.SprintSpeed;
        else
            currentSpeed = Constants.NormalSpeed;
    }

    private void HandleMovement()
    {
        var movementVector = GameInput.Instance.GetMovementVector();
        rb2.linearVelocity = movementVector * currentSpeed;

        if (Mathf.Abs(movementVector.x) > minMovingSpeed || Mathf.Abs(movementVector.y) > minMovingSpeed)
        {
            // ������� ��������� ������������ �������� (����� ���������)
            if (Mathf.Abs(movementVector.y) > minMovingSpeed)
            {
                isRunningW = movementVector.y > 0;
                isRunningS = movementVector.y < 0;
                // ���� �������� �����/����, ���������� �������������� �����������
                isRunningA = false;
                isRunningD = false;
            }
            else
            {
                // ���� ��� ������������� ��������, ��������� ��������������
                isRunningD = movementVector.x > 0;
                isRunningA = movementVector.x < 0;
            }
        }
        else
        {
            isRunningW = false;
            isRunningA = false;
            isRunningS = false;
            isRunningD = false;
        }
    }

    public bool IsRunningW() => isRunningW;
    public bool IsRunningA() => isRunningA;
    public bool IsRunningS() => isRunningS;
    public bool IsRunningD() => isRunningD;
}