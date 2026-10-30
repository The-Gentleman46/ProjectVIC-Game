using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class snakeMovement : MonoBehaviour
{
    [SerializeField] private float rotationOffset;
    [SerializeField] private spawnFood spawnFoodScript;
    [SerializeField] GameObject snakeBody;
    [SerializeField] GameObject snakeHead;
    public List<Transform> segments = new List<Transform>();
    public Queue<Vector2> positionHistory = new Queue<Vector2>();
    private float movementCooldown = 0.3f; // Adjust for speed
    private float timeSinceLastMove = 0f;
    private int initialLength = 3;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 direction = Vector2.up;
    private bool canCollide = false;

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
        // Move up
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
            moveInput = Vector2.up;
            rotationOffset = 0f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            //rb.MoveRotation(rotationOffset);
        }
        // Move down
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
            moveInput = Vector2.down;
            rotationOffset = 180f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            //rb.MoveRotation(rotationOffset);
        }
        // Move left
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
            moveInput = Vector2.left;
            rotationOffset = 90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            //rb.MoveRotation(rotationOffset);
        }
        // Move right
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
            moveInput = Vector2.right;
            rotationOffset = -90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            //rb.MoveRotation(rotationOffset);
        }
    }
    private void FixedUpdate()
    {
        timeSinceLastMove += Time.fixedDeltaTime;

        // Move head in grid steps
        if (timeSinceLastMove >= movementCooldown && moveInput != Vector2.zero)
        {
            transform.position = new Vector3(
                Mathf.Round(transform.position.x + direction.x),
                Mathf.Round(transform.position.y + direction.y),
                0.0f
            );

            positionHistory.Enqueue(transform.position);
            timeSinceLastMove = 0f;
        }

        // Body segments follow the position history
        for (int i = 1; i < segments.Count; i++)
        {
            if (positionHistory.Count > i - 1)
            {
                segments[i].position = positionHistory.ElementAt(i - 1);
            }
        }

        // Remove old positions from history
        if (positionHistory.Count > segments.Count)
        {
            positionHistory.Dequeue();
        }

    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            canCollide = true;
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
            // Destroy food
            Destroy(collision.gameObject);
            // Remove occupied position from hashSet
            spawnFoodScript.OccPosition.Remove(collision.gameObject.transform.position);
            // Add a segment
            Grow();
            spawnFoodScript.numberOfSpawned--;
        }
        if (canCollide)
        {
            if (collision.gameObject.CompareTag("Body"))
            {
                Debug.Log("Collided with self");
                Destroy(gameObject);
                UnityEngine.SceneManagement.SceneManager.LoadScene("gameOver");
            }
        }

    }

    public void Grow()
    {
        // Creates a new segment upon consumption of a fruit
        Transform newSegment = Instantiate(snakeBody.transform);
        // Sets the new position to that of the one in front of it (the * 2f at the end is me trying to make them more spaced apart)
        newSegment.position = segments[0].position;
        // Add to the list of segments
        segments.Add(newSegment);

    }
}
