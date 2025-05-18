using UnityEngine;

public class Player : MonoBehaviour
{
    // review: грязный прием. Можем ли сделать не синглтоном?
    public static Player Instance { get; private set; }

    public bool canMove = true;

    private Rigidbody2D rb2;

    private float currentSpeed;

    private float minMovingSpeed = 0.1f;

    // review: кажется, что нужно называть исходя из направления, а не прибинженной кнопки
    // review: можем ли хранить только одно поле - Direction?
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

    private void FixedUpdate() => HandleMovement();

    public void ChangeSpeed(float speed)
    {
        currentSpeed = speed;
    }

    private void Update()
    {
        //canMove = true;
    }

    private void HandleMovement()
    {
        if (canMove)
        {

            var movementVector = GameInput.Instance.GetMovementVector();
            rb2.velocity = movementVector * currentSpeed;

            // review: стоит выделить метод IsPlayerMoving
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
    }

    public bool IsRunningW() => isRunningW;
    public bool IsRunningA() => isRunningA;
    public bool IsRunningS() => isRunningS;
    public bool IsRunningD() => isRunningD;
}