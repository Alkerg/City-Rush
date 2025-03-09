using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float railDistance = 1.43f;
    public float moveSpeed = 7f; 
    public float jumpForce = 8f; 
    public LayerMask groundLayer;
    public Transform checkGround;
    public float jumpBufferCounter;
    public TouchControl touchControl;
    
    private float jumpBufferTime = 0.3f;
    private AudioSource audioSource;
    private Rigidbody rb;
    private bool isGrounded;
    private int rail = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!LevelManager.isGameRunning) return;

        // Right and left movement
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || touchControl.swipeDirection == TouchControl.Directions.left) && rail > 0)
        {
            rail--;
            audioSource.Play();
            StartCoroutine(Move());
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || touchControl.swipeDirection == TouchControl.Directions.right) && rail < 2)
        {
            rail++;
            audioSource.Play();
            StartCoroutine(Move());
        }



        isGrounded = Physics.CheckSphere(checkGround.transform.position, 0.05f, groundLayer);

        // Up and down movement
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || touchControl.swipeDirection == TouchControl.Directions.up)
        {
            jumpBufferCounter = jumpBufferTime;
            audioSource.Play();
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || touchControl.swipeDirection == TouchControl.Directions.down)
        {
            rb.velocity = new Vector3(rb.velocity.x, -jumpForce, rb.velocity.z);
            audioSource.Play();
        }

        //Jump buffer
        if (isGrounded && jumpBufferCounter > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpBufferCounter = 0;
        }
    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        Vector3 targetPosition = new Vector3(railDistance * (rail - 1), transform.position.y, transform.position.z); // -1.43, 0, 1.43
        while (elapsedTime < moveSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

    }

}
