using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D rb2;
	
	private float currentSpeed;
	
	private void Awake()
	{
		rb2 = GetComponent<Rigidbody2D>();
		rb2.freezeRotation = true;
		currentSpeed = Constants.NormalSpeed;
	}
	
	private void Update()
	{
		ChangeSpeed();
	}

	private void FixedUpdate()
	{
		var movementVector = GameInput.Instance.GetMovementVector();
		rb2.linearVelocity = movementVector * currentSpeed;
	}

	private void ChangeSpeed()
	{
		if (GameInput.Instance.IsSneaking())
			currentSpeed = Constants.SneakySpeed;
		else if (GameInput.Instance.IsSprinting())
			currentSpeed = Constants.SprintSpeed;
		else
			currentSpeed = Constants.NormalSpeed;
	}
}