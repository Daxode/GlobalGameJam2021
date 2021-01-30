using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private CharacterController Sparrow;
	public CharacterController Windston;

	public float speed      = 12f;
	public float gravity    = -10f;
	public float jumpHeight = 2f;

	private Camera cam;

	public Transform groundCheck;
	public float     groundDistance = 0.4f;
	public LayerMask groundMask;

	Vector3 velocity;
	bool    isGrounded;

	private void Start() {
		cam = Camera.main;
		Sparrow = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update() {
		float x;
		float z;
		bool  jumpPressed = false;

		x = Input.GetAxis("Horizontal");
		z = Input.GetAxis("Vertical");
		jumpPressed = Input.GetButtonDown("Jump");

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocity.y < 0) {
			velocity.y = -2f;
		}

		Vector3 move = cam.transform.right * x + cam.transform.forward * z;
		move = new Vector3(move.x, 0, move.z);
		if (cam.GetComponent<CamSwap>().Sparrow.Priority == 1) {
			Sparrow.Move(move * (speed * Time.deltaTime));
		}
		else {
			Windston.Move(move * (speed * Time.deltaTime));
		}

		if (jumpPressed && isGrounded) {
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

		velocity.y += gravity * Time.deltaTime;

		Sparrow.Move(velocity * Time.deltaTime);
		Windston.Move(velocity * Time.deltaTime);
	}
}