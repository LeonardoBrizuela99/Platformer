using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public float rotateSpeed=5f;

    private Vector3 moveDirection;

    public CharacterController characterController;
    public Camera playerCamara;
    public GameObject playerModel;
    public Animation animator;

    private void Awake()
    {
        instance = this;
    }

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
