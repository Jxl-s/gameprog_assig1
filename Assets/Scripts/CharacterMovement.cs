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
    public float mouseSensitivyY = 5.0f;

    private Vector3 move;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Animator animator;

    private float walkSpeed = 4;
    private float runSpeed = 6;

    private bool jumped = false;
    private bool doubleJumped = false;

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

        UpdateRotation();
        ProcessCamera();
    }

    public void LateUpdate()
    {
        UpdateAnimator();
    }

    void UpdateRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivyX, 0, Space.Self);

        // rotate the follow part
        float rotationX = followPart.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensitivyY;
        if (rotationX > 45 && rotationX < 90)
        {
            rotationX = 45;
        } else if (rotationX < 325 && rotationX >= 90)
        {
            rotationX = 325;
        }

        followPart.transform.localEulerAngles = new Vector3(rotationX, 0, 0);

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
            jumped = false;
            if (doubleJumped)
            {
                doubleJumped = false;

                if (!canDoubleJump)
                {
                    HUDManager.Instance.SetDoubleJump(HUDManager.DoubleState.False);
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumped = true;
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
            if (canDoubleJump && jumped && !doubleJumped)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    doubleJumped = true;
                    canDoubleJump = false;

                    HUDManager.Instance.SetDoubleJump(HUDManager.DoubleState.Used);
                    gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
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
        // Calculate desired camera position (behind character)
        Vector3 desiredPosition = followPart.transform.position - followPart.transform.forward * 3;

        // Move camera
        Camera.main.transform.position = desiredPosition;
        Camera.main.transform.LookAt(followPart.transform.position);
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