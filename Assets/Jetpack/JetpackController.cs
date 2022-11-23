using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif
using StarterAssets;

public class JetpackController : MonoBehaviour
{
    private Vector3 playerMovementInput;
    public ThirdPersonController thirdPersonController;
    public Transform cam;
    public float rotationSpeed;
    public float jetForce;
    float turnSmoothVelocity;
    public Rigidbody rb;
    public BoxCollider bCollider;
    [SerializeField] GameObject human;
    [SerializeField] GameObject jetPack;
    float spacePressed;
    bool hasPlayer;

    void Update()
    {
        if (thirdPersonController.onJetPack)
        {
            rb.useGravity = true;
            GetIntoJet();
            bCollider.enabled = true;
            hasPlayer = true;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            spacePressed = Input.GetAxisRaw("Jump");
            playerMovementInput = new Vector3(horizontal, spacePressed, vertical);

            if (Input.GetKeyDown(KeyCode.F) && hasPlayer)
            {
              GetOutOfJet();
            }
        }
    }
	private void FixedUpdate()
	{
        if(hasPlayer)
            MoveJet(spacePressed);
    }
	private void MoveJet(float Space)
	{
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * jetForce * Time.deltaTime;
        rb.velocity = new Vector3(moveVector.x, moveVector.y, moveVector.z).normalized;

        float targetAngle = Mathf.Atan2(playerMovementInput.x, playerMovementInput.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (Space > 0)
		{
			moveVector += Vector3.up * jetForce * Time.deltaTime;
        }
    }
    private void GetOutOfJet()
    {
        thirdPersonController.onJetPack= false;
        human.transform.position = gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.left);
        thirdPersonController.charController.enabled = true;
        thirdPersonController.onVehicle = false;
        thirdPersonController.playerFollowCamera.SetActive(true);
        thirdPersonController.jetFollowCamera.SetActive(false);
    }
    private void GetIntoJet()
    {

        thirdPersonController.onJetPack = true;
        thirdPersonController.jetFollowCamera.SetActive(true);
        thirdPersonController.playerFollowCamera.SetActive(false);
        thirdPersonController.charController.enabled = false;
		human.transform.parent = base.transform;
        human.transform.position = new Vector3(transform.position.x, transform.position.y - 1.3f,transform.position.z);
		human.transform.rotation = jetPack.transform.rotation;

	}
/*    void JumpGravityMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !jumpPressed)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1f * Time.fixedDeltaTime;
        }
    }*/
}