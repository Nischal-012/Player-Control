using StarterAssets;
using UnityEngine;

public class HoverController : MonoBehaviour
{
	[SerializeField] private float accleration;
	[SerializeField] private float rotationRate;
	[SerializeField] float turnRotationAngle;
	[SerializeField] float turnRotationSeekSpeed;
	[SerializeField] Rigidbody rb;
	[SerializeField] ThirdPersonController thirdPersonController;
	[SerializeField] GameObject human;
	[SerializeField] GameObject Hover;
	[SerializeField] Transform cam;
	[SerializeField] bool rotationControl;

	float vertical;
	float horizontal;
	bool hasPlayer;
	private float rotationVelocity;
	private Vector3 direction;

	private void FixedUpdate()
	{
		if (hasPlayer)
		{
			GetIntoHover();
			if (Physics.Raycast(transform.position, transform.up * -1, 3f))
			{
				Vector3 forwardForce = transform.forward * accleration * vertical;
				forwardForce = forwardForce * Time.fixedDeltaTime * rb.mass;

				rb.AddForce(forwardForce);
			}

			if (rotationControl)
			{
				if (direction != Vector3.zero)
				{
					Vector3 sideWaysForce = transform.right * accleration * horizontal;
					sideWaysForce = sideWaysForce * Time.fixedDeltaTime * rb.mass;

					rb.AddForce(sideWaysForce);
					float targetAngle = Mathf.Atan2(0, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
					float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, turnRotationSeekSpeed);
					transform.rotation = Quaternion.Euler(0f, angle, 0f);
				}
			}
			else
			{
				Vector3 turnTorque = Vector3.up * rotationRate * horizontal;
				turnTorque = turnTorque * Time.fixedDeltaTime * rb.mass;
				rb.AddTorque(turnTorque);
				Vector3 newRotation = transform.eulerAngles;
				newRotation.z = Mathf.SmoothDampAngle(newRotation.z, horizontal * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
				transform.eulerAngles = newRotation;
			}


		}
	}

	private void Update()
	{
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");
		direction = new Vector3(horizontal, 0, vertical).normalized;
		if (thirdPersonController.onHoverBoard)
			hasPlayer = true;
		if (Input.GetKeyDown(KeyCode.F) && hasPlayer)
		{
			GetOutOfHover();
		}
	}

	private void GetOutOfHover()
	{
		thirdPersonController.onJetPack = false;
		human.transform.position = gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.left);
		thirdPersonController.charController.enabled = true;
		thirdPersonController.playerFollowCamera.SetActive(true);
		thirdPersonController.hoverFollowCamera.SetActive(false);
		hasPlayer = false;
		thirdPersonController.onHoverBoard = false;
	}

	private void GetIntoHover()
	{
		thirdPersonController.hoverFollowCamera.SetActive(true);
		thirdPersonController.playerFollowCamera.SetActive(false);
		human.transform.position = base.transform.position;
		human.transform.rotation = base.transform.rotation;
		thirdPersonController.onHoverBoard = true;
		thirdPersonController.charController.enabled = false;

	}
}
