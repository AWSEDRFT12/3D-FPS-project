using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 10f;
    public float gravityModifier = 1f;
    public float mouseSensitivity = 1f;
    public Transform theCamera;
    public Transform groudCheckpoint;
    public LayerMask whatIsGround;
    private bool canPlayerJump;
    private Vector3 moveInput; 
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent <CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Store the y velocity
        float yVelocity = moveInput.y; 

        

        //Player movement
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 fowardDirection = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalDirection = transform.right * Input.GetAxis("Horizontal");

        moveInput =(fowardDirection + horizontalDirection).normalized;
        moveInput *= moveSpeed;

        //Player Jumping setup
        moveInput.y = yVelocity;
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if(characterController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        //Checking to see if player can jump
        canPlayerJump = Physics.OverlapSphere(groudCheckpoint.position, 0.50f, whatIsGround).Length > 0;

        //Apply a jump force to player
        if(Input.GetKeyDown(KeyCode.Space) && canPlayerJump)
        {
            moveInput.y = jumpForce;
        }

        characterController.Move(moveInput * Time.deltaTime);

        //Control camera rotation 
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        //Player Rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        //Camera Rotation
        theCamera.rotation = Quaternion.Euler(theCamera.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f)); 
    }
}
