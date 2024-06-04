using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
   public float horizontalMove;
   public float verticalMove;
    private Vector3 playerInput;
    //  public static PlayerController instance;
    public CharacterController player;
    public float playerSpeed;

    private Vector3 movePlayer;

    public Camera mainCamera;
    private Vector3 camFoward;
    private Vector3 camRight;
 
    void Start()
    {
        player=GetComponent<CharacterController>();
    }

   
    void Update()
    {

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
    
        playerInput = new Vector3(horizontalMove,0,verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camFoward;

        player.transform.LookAt(player.transform.position + movePlayer);
        
        player.Move(movePlayer *playerSpeed * Time.deltaTime);

        Debug.Log(player.velocity.magnitude);
        
    }

    void camDirection()
    {
        camFoward=mainCamera.transform.forward;
        camRight=mainCamera.transform.right;

        camFoward.y=0;
        camRight.y=0;

        camFoward = camFoward.normalized;
        camRight = camRight.normalized;
    }
}