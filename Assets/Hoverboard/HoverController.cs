using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class HoverController : MonoBehaviour
{
	public float accleration;
	public float rotationRate;
	public float turnRotationAngle;
	public float turnRotationSeekSpeed;
	public float vertical;
	public float horizontal;

	public Rigidbody rb;
	private float rotationVelocity;
	public ThirdPersonController thirdPersonController;
	public bool hasPlayer;
	[SerializeField] GameObject human;
	[SerializeField] GameObject Hover;
	private void FixedUpdate()
	{
		if (hasPlayer)
		{
			GetIntoHover();
			if (Physics.Raycast(transform.position, transform.up * -1, 3f))
			{
				rb.drag = 1;

				Vector3 forwardForce = transform.forward * accleration * vertical;
				forwardForce = forwardForce * Time.fixedDeltaTime * rb.mass;

				rb.AddForce(forwardForce);
			}
			else
				rb.drag = 0;
			Vector3 turnTorque = Vector3.up * rotationRate * horizontal;
			turnTorque = turnTorque * Time.fixedDeltaTime * rb.mass;
			rb.AddTorque(turnTorque);

			Vector3 newRotation = transform.eulerAngles;
			newRotation.z = Mathf.SmoothDampAngle(newRotation.z, horizontal * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
			transform.eulerAngles = newRotation;

		}
	}

	private void Update()
	{

		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");
		if (thirdPersonController.onHoverBoard)
			hasPlayer = true;
		if (Input.GetKeyDown(KeyCode.F)  && hasPlayer)
		{
			GetOutOfHover();
		}
	}
    
    private void GetOutOfHover()
	{
		thirdPersonController.onJetPack = false;
		human.transform.position = gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.left);
		thirdPersonController.charController.enabled = true;
		thirdPersonController.onVehicle = false;
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
