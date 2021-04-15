using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Controls")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticalSensitivity;

    public CharacterController controller;
    public float maxSpeed = 3000.0f;
    public float gravity = -70.0f;
    public float jumpHeight = 25.0f;

    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public Vector3 velocity;
    public bool isGrounded;

    private float jumpForce = 5f;

    public GameObject fire;

    public GameObject bulletPrefab;
    public Camera playerCamera;

    [Header("HealthBar")]
    public HealthBarScreenSpaceController healthBar;

    [Header("Player Abilities")]
    [Range(0, 100)]
    public float health = 100f;

    [Header("Input Options")]
    public OptionsSO currentOptions;
    public KeyCode forwardKey;
    public KeyCode backwardKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode boostKey;
    public KeyCode swapKey;

    public GameController gameController;
    private bool isWebGL = false;

    // Observer Pattern - Observable(Subject)
	public delegate void FireDelegate();
	public static event FireDelegate FireEvent;

    [Header("Sound Effects")]
    public AudioClip takeDamageSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        LoadCurrentOptions();
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

        if (isWebGL)
        {
            // Move forward
            if(Input.GetKey(forwardKey))
            {
                Vector3 move = transform.forward;
                controller.Move(move * maxSpeed * Time.deltaTime);
            }

            // Move backward
            if(Input.GetKey(backwardKey))
            {
                Vector3 move = transform.forward * -1;
                controller.Move(move * maxSpeed * Time.deltaTime);
            }

            // Move left
            if(Input.GetKey(leftKey))
            {
                Vector3 move = transform.right * -1;
                controller.Move(move * maxSpeed * Time.deltaTime);
            }

            // Move right
            if(Input.GetKey(rightKey))
            {
                Vector3 move = transform.right;
                controller.Move(move * maxSpeed * Time.deltaTime);
            }

            // Jump
            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                Jump();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
        else
        {
            float x = joystick.Horizontal;
            float z = joystick.Vertical;
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * maxSpeed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // player fell outside of the game map
        if (transform.position.y < 0.0f)
        {
            TakeDamage(100);
        }
    }

    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
    }

    public void Fire()
    {
        if (FireEvent != null)
		{
			FireEvent();
		}
        GameObject bulletObject = Instantiate(bulletPrefab);
        bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * -20 + playerCamera.transform.up * -2 + playerCamera.transform.right * -3;
        bulletObject.transform.forward = playerCamera.transform.forward;

        GunController gun = FindObjectOfType<GunController>();
        gun.Shoot();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void TakeDamage(float damage)
    {
        GetComponent<AudioSource>().PlayOneShot(takeDamageSoundEffect); // play sound effect
        health -= damage;
        healthBar.TakeDamage(damage);
        if (health < 0)
        {
            health = 0;
            gameController.GameOver();
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        healthBar.SetHealthValue(health);
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

    // Load the keys from currentOptions
    public void LoadCurrentOptions()
    {
        forwardKey = currentOptions.forwardKey;
        backwardKey = currentOptions.backwardKey;
        leftKey = currentOptions.leftKey;
        rightKey = currentOptions.rightKey;
        jumpKey = currentOptions.jumpKey;
        boostKey = currentOptions.boostKey;
        swapKey = currentOptions.swapKey;
    }
}