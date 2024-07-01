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
    private int jumpCount = 0;

    //private Animator animator;

    public float fallLimit = -5.0f;
    private Vector3 startPosition;
    public FadeTransition fadeTransition;

    private AudioSource audioSource;

    public AudioClip jumpSound;
    void Start()
    {
        player = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        //animator = GetComponent<Animator>();
        startPosition = transform.position;
    }


    void Update()
    {
        isGrounded = player.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;
        }

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camFoward;

        player.transform.LookAt(player.transform.position + movePlayer);

        player.Move(movePlayer * playerSpeed.speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < 2))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount++;
            PlayJumpSound();
            // animator.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);

        Debug.Log(player.velocity.magnitude);

        if (transform.position.y < fallLimit)
        {
            StartCoroutine(Respawn());
        }
        //if (playerInput.magnitude > 0)
        //{
        //    animator.SetBool("isRunning", true);
        //}
        //else
        //{
        //    animator.SetBool("isRunning", false);
        //}

        //if (isGrounded)
        //{
        //    animator.SetBool("isJumping", false);
        //}
        //else
        //{
        //    animator.SetBool("isJumping", true);
        //}

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
    private void PlayJumpSound()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private IEnumerator Respawn()
    {
        yield return StartCoroutine(fadeTransition.FadeOut());
        transform.position = startPosition;
        yield return StartCoroutine(fadeTransition.FadeIn());
    }
}