using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //[SerializeField]
   // private float movementSpeed;
   // [SerializeField]
   // private float maxSpeed;

    //[SerializeField]
    //private float jumpForce;
    // Inputsystem inputActions;
   // InputAction move;

   // private Rigidbody rb;
  //  private Vector3 forceDirection;


    public float jumpForce;
    public float moveSpeed;
    
    public float gravityScale = 5f;
    public float rotateSpeed=5f;

    private Vector3 moveDirection;

    public CharacterController characterController;
    public Camera playerCamara;
    public GameObject playerModel;
    public Animation animator;

     void Awake()
     {
        instance = this;
       // rb = GetComponent<Rigidbody>();
        //inputActions =new InputSystem();    
     }

    //private void OnEnable()
    //{
    //    inputActions.Land.Jump.performed += OnJump;
    //    inputActions.Enable();
    //}
    //private void OnDisable()
    //{
    //    inputActions.Land.Jump.performed -= OnJump;
    //    inputActions.Disable();
    //}
    //void OnJump(InputAction.CallbackContext ctx)
    //{

    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;

        //moveDirection = new Vector3 (Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical"))+transform.right*Input.GetAxisRaw("Horizontal");
        moveDirection = moveDirection * moveSpeed;
        moveDirection.y = yStore;

        //forceDirection += move.ReadValue<Vector2>().x * transform.right * movementSpeed;
        //forceDirection += move.ReadValue<Vector2>().y * transform.forward * movementSpeed;

        //rb.AddForce(forceDirection,ForceMode.Impulse);
        //forceDirection = Vector3.zero;

        //Vector3 horizontalVel = rb.velocity;
        //horizontalVel.y = 0;

        //if (horizontalVel.sqrMagnitude>maxSpeed*maxSpeed)
        //{
        //    rb.velocity = horizontalVel.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        //}

         if (characterController.isGrounded )
         {
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
         }
       

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        characterController.Move(moveDirection * Time.deltaTime);

        if(Input.GetAxisRaw("Horizontal")!=0|| Input.GetAxisRaw("Vertical")!=0)
        {
            transform.rotation = Quaternion.Euler(0f, playerCamara.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed*Time.deltaTime);
        }

        
    }
}
