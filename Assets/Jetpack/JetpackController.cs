using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
#endif
using StarterAssets;

public class JetpackController : MonoBehaviour
{
	[SerializeField] private ThirdPersonController thirdPersonController;
	[SerializeField] private Transform cam;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float jetForce;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private BoxCollider bCollider;
	[SerializeField] GameObject human;
	[SerializeField] GameObject jetPack;
	private float gravity = 1;
	private Vector3 playerMovementInput;
	float spacePressed;
	[SerializeField]bool hasPlayer;
	float turnSmoothVelocity;
	Vector3 direction;

	void Update()
	{
		if (thirdPersonController.onJetPack)
		{
			GetIntoJet();
			hasPlayer = true;
			float horizontal = Input.GetAxisRaw("Horizontal");
			float vertical = Input.GetAxisRaw("Vertical");
			spacePressed = Input.GetAxisRaw("Jump");
			direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
			playerMovementInput = new Vector3(horizontal, spacePressed, vertical);

			if (Input.GetKeyDown(KeyCode.F) && hasPlayer)
			{
				GetOutOfJet();
			}
		}
	}
	private void FixedUpdate()
	{
		if (hasPlayer)
			MoveJet(spacePressed);
	}
	private void MoveJet(float Space)
	{

		Vector3 moveVector = transform.TransformDirection(playerMovementInput) * jetForce * Time.fixedDeltaTime;
		rb.velocity = new Vector3(moveVector.x, moveVector.y, moveVector.z).normalized;
		if (direction != Vector3.zero)
		{
			float targetAngle = Mathf.Atan2(0, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSpeed);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
		}

		if (Space > 0)
		{
			moveVector += Vector3.up * jetForce * Time.fixedDeltaTime;
		}
		else
		{
			moveVector -= Vector3.down * jetForce * Time.fixedDeltaTime;
		}
	}

	private void GetOutOfJet()
	{
		hasPlayer = false;
		bCollider.enabled = false;
		thirdPersonController.onJetPack = false;
		human.transform.position = gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.left);
		thirdPersonController.charController.enabled = true;
		thirdPersonController.transform.parent = null;
		thirdPersonController.playerFollowCamera.SetActive(true);
		thirdPersonController.jetFollowCamera.SetActive(false);
	}
	private void GetIntoJet()
	{
		bCollider.enabled = true;
		thirdPersonController.onJetPack = true;
		thirdPersonController.jetFollowCamera.SetActive(true);
		thirdPersonController.playerFollowCamera.SetActive(false);
		thirdPersonController.charController.enabled = false;
		human.transform.parent = base.transform;
		human.transform.position = new Vector3(transform.position.x, transform.position.y - 1.3f, transform.position.z);
		human.transform.rotation = jetPack.transform.rotation;

	}
}
