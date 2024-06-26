using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float horizontalMove;
    [SerializeField] public float verticalMove;
    [SerializeField] private Vector3 playerInput;

    [SerializeField] public CharacterController player;
    public PlayerSpeed playerSpeed;

    [SerializeField] private Vector3 movePlayer;

    [SerializeField] public Camera mainCamera;
    [SerializeField] private Vector3 camFoward;
    [SerializeField] private Vector3 camRight;

    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravity = -15f;
    private Vector3 velocity;
    private bool isGrounded;
    void Start()
    {
        player = GetComponent<CharacterController>();
    }


    void Update()
    {
        isGrounded = player.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camFoward;

        player.transform.LookAt(player.transform.position + movePlayer);

        player.Move(movePlayer * playerSpeed.speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);

        Debug.Log(player.velocity.magnitude);

    }

    void camDirection()
    {
        camFoward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camFoward.y = 0;
        camRight.y = 0;

        camFoward = camFoward.normalized;
        camRight = camRight.normalized;
    }

    private void OnValidate()
    {
        if (gravity > -9.81f)
        {
            gravity = -9.81f;
        }

        if (jumpHeight < 1.0f)
        {
            jumpHeight = 1.0f;
        }
    }
}
