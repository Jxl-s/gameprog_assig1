using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{

    public bool canDoubleJump = false;
    public GameObject followPart;

    public GameObject emitter;
    public Vector3 gravity;
    public Vector3 playerVelocity;
    public bool groundedPlayer;

    public float mouseSensitivyX = 5.0f;
    public float mouseSensitivyY = 0.2f;

    private Vector3 move;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Animator animator;

    private float walkSpeed = 4;
    private float runSpeed = 6;


    private void Start()
    {
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (animator.applyRootMotion == false)
        {
            ProcessMovement();
        }

        ProcessCamera();
        UpdateRotation();
    }

    public void LateUpdate()
    {
        UpdateAnimator();
    }
    void UpdateRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivyX, 0, Space.Self);

        // my attempt at supporting vertical rotation
        // Vector3 eulers = followPart.transform.rotation.eulerAngles;
        // followPart.transform.rotation = Quaternion.Euler(Input.GetAxis("Mouse Y") * mouseSensitivyY, eulers.y, eulers.z);
    }

    void UpdateAnimator()
    {
        float curSpeed = animator.GetFloat("Speed");
        if (move != Vector3.zero)
        {
            if (GetMovementSpeed() == runSpeed)
            {
                curSpeed = Mathf.Lerp(curSpeed, 1, Time.deltaTime * 5.0f);
            }
            else
            {
                curSpeed = Mathf.Lerp(curSpeed, 0.5f, Time.deltaTime * 5.0f);
            }
        }
        else
        {
            curSpeed = Mathf.Lerp(curSpeed, 0f, Time.deltaTime * 5.0f);
        }

        animator.SetFloat("Speed", curSpeed);
        // bool isGrounded = controller.isGrounded;
        // animator.SetBool("IsGrounded", isGrounded);
        // if (Input.GetButtonDown("Fire2"))
        // {
        //     animator.applyRootMotion = true;
        //     animator.SetTrigger("doRoll");
        // }
    }

    void ProcessMovement()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Moving the character forward according to the speed
        float speed = GetMovementSpeed();

        // Get the camera's forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Make sure to flatten the vectors so that they don't contain any vertical component
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to ensure consistent speed in all directions
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on input and camera orientation
        Vector3 moveDirection = (cameraForward * Input.GetAxis("Vertical")) + (cameraRight * Input.GetAxis("Horizontal"));

        // Apply the movement direction and speed
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            if (Input.GetButtonDown("Jump"))
            {
                gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                animator.SetTrigger("IsJumping");
            }
            else
            {
                // Dont apply gravity if grounded and not jumping
                gravity.y = -1.0f;
            }
        }
        else
        {
            // Since there is no physics applied on character controller we have this applies to reapply gravity
            gravity.y += gravityValue * Time.deltaTime;
            if (canDoubleJump)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                    canDoubleJump = false;
                    animator.SetTrigger("IsDoubleJumping");
                }
            }
        }

        // Apply gravity and move the character
        playerVelocity = gravity * Time.deltaTime + movement;
        controller.Move(playerVelocity);

        // Send the speed to the animator
    }

    void ProcessCamera()
    {
        // third person camera
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position + new Vector3(0, 1.5f, -6), 0.1f);
    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Fire3"))// Left shift
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }

    public void Kill()
    {
        emitter.transform.position = transform.position + new Vector3(0, 2, 0);
        emitter.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
    }

    public void Freeze()
    {
        gameObject.SetActive(false);
    }
}