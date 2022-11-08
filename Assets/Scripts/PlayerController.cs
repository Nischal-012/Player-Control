using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator anim;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    public GameObject particleLeg;
    public GameObject particleShoulder;
    float turnSmoothVelocity;
    public bool moving;

    private CharacterController characterController;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            moving = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * Time.deltaTime * speed);
            Walk();
        }
        else
        {
            moving = false;
            Idle();
        }
    }
    private void Walk()
    {
        anim.SetFloat("Speed", 1);
        if (anim.GetBool("isSwimming") == true)
        {
            anim.SetFloat("Swim", 0);
        }
           
    }
    private void Idle()
    {
        anim.SetFloat("Speed", 0);
        if (anim.GetBool("isSwimming") == true)
        {
            anim.SetFloat("Swim", 1);
        }
    }
}
