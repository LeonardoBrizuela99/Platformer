using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]public float horizontalMove;
    [SerializeField]public float verticalMove;
    [SerializeField]private Vector3 playerInput;
   
    [SerializeField]public CharacterController player;
    [SerializeField]public float playerSpeed;
    
    [SerializeField]private Vector3 movePlayer;

    [SerializeField]public Camera mainCamera;
    [SerializeField]private Vector3 camFoward;
    [SerializeField] private Vector3 camRight;
 
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