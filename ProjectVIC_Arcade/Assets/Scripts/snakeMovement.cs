using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class snakeMovement : MonoBehaviour
{
    [SerializeField] private float rotationOffset;
    [SerializeField] private spawnFood spawnFoodScript;
    [SerializeField] GameObject snakeBody;
    [SerializeField] GameObject snakeHead;
    [SerializeField] List<Transform> segments = new List<Transform>();
    private int initialLength = 3;
    private float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector3 direction = Vector3.up;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Adds the head to the segments list
        segments.Add(transform);
        // Adds three initial segments
        for(int i = 0; i < initialLength; i++)
        {
            Grow();
        }
        
    }

    void Update()
    {
        rb.linearVelocity = moveInput * speed;

        // Move up
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.down)
        {
            direction = Vector3.forward;
            moveInput = Vector2.up;
            rotationOffset = 0f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        // Move down
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.up)
        {
            direction = Vector3.back;
            moveInput = Vector2.down;
            rotationOffset = 180f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        // Move left
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.right)
        {
            direction = Vector3.left;
            moveInput = Vector2.left;
            rotationOffset = 90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        // Move right
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.left)
        {
            direction = Vector3.right;
            moveInput = Vector2.right;
            rotationOffset = -90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
    }

    private void FixedUpdate()
    {
        Vector3 offset = new Vector3(0.5f, 0.5f, 0.0f);

        // Moves the player by 1 unit every movement cycle but collides with the wall almost immedietly due to how small the level is
        /*
        transform.position = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y),
            0.0f
        );*/

        // Sets each segments position to that of the one in front of it to make it so they all follow each other
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collided with wall");
            Destroy(gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameOver");
        }
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Collided with food");
            Destroy(collision.gameObject);
            Grow();
            spawnFoodScript.numberOfSpawned--;
        }
        if (collision.gameObject.CompareTag("Body"))
        {
            Debug.Log("Collided with self");
            Destroy(gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameOver");
        }
    }

    public void Grow()
    {
        // Creates a new segment upon consumption of a fruit
        Transform newSegment = Instantiate(snakeBody.transform);
        // Sets the new position to that of the one in front of it (the * 2f at the end is me trying to make them more spaced apart)
        newSegment.position = segments[segments.Count - 1].position - direction * 2f;
        // Add to the list of segments
        segments.Add(newSegment);

    }
}
