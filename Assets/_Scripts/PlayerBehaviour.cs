using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;

    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public Vector3 velocity;
    public bool isGrounded;
    
    private float jumpForce = 2.0f; 

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame - once every 16.6666ms
    // 1000ms for each second
    // approximately updates 60 times per second = 60fps
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * maxSpeed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    // Detect collider
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        switch(hit.gameObject.tag) {
            case "JumpFan":
                Debug.Log("Collide on a JumpFan");
                velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity * jumpForce);
                hit.gameObject.GetComponent<AudioSource>().Play();  // Play sound effect
                break;
            default:
                break;
        }
    }
}
