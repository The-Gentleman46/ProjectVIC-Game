using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.LowLevelPhysics2D;

public class snakeMovement : MonoBehaviour
{
    [SerializeField] private float rotationOffset;
    [SerializeField] private spawnFood spawnFoodScript;
    [SerializeField] GameObject snakeBody;
    [SerializeField] GameObject snakeHead;
    public List<Transform> segments = new List<Transform>();
    [SerializeField] Queue<Vector2> positionHistory = new Queue<Vector2>();
    private float movementCooldown = 0.3f; // Adjust for speed
    private float timeSinceLastMove = 0f;
    private int initialLength = 3;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 direction = Vector2.up;
    private bool inputMadeThisFrame = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Adds the head to the segments list
        segments.Add(transform);

        // Adds three initial segments
        for (int i = 0; i < initialLength; i++)
        {
            Transform newSegment = Instantiate(snakeBody.transform);
            newSegment.name = i.ToString();
            newSegment.position = new Vector3(0.0f, -i - 1, 0.0f);
            segments.Add(newSegment);
        }

        // Pre-populate position history with initial positions (tail to head)
        // This prevents segments from jumping on the first frame
        positionHistory.Clear();
        for (int j = segments.Count - 1; j >= 0; j--)
        {
            positionHistory.Enqueue(segments[j].position);
        }

        segments[1].GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        if (inputMadeThisFrame)
            return;
        // Move up
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
            moveInput = Vector2.up;
            rotationOffset = 0f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            inputMadeThisFrame = true;
        }
        // Move down
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
            moveInput = Vector2.down;
            rotationOffset = 180f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            inputMadeThisFrame = true;
        }
        // Move left
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
            moveInput = Vector2.left;
            rotationOffset = 90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            inputMadeThisFrame = true;
        }
        // Move right
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
            moveInput = Vector2.right;
            rotationOffset = -90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            inputMadeThisFrame = true;
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
            inputMadeThisFrame = false;
        }

        // Body segments follow the position history
        for (int i = 1; i < segments.Count; i++)
        {
            // Each segment follows the head's position from i moves ago
            if (positionHistory.Count >= i)
            {
                segments[i].position = positionHistory.ElementAt(positionHistory.Count - i);
            }
        }

        // Remove old positions from history
        if (positionHistory.Count > segments.Count)
        {
            positionHistory.Dequeue();
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
        if (collision.gameObject.CompareTag("Body"))
        {
            Debug.Log("Collided with self");
            Destroy(gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameOver");
        }
    }

    public void Grow()
    {
        // New segment starts at the position of the last segment in the chain
        Transform newSegment = Instantiate(snakeBody.transform);
        newSegment.position = segments[segments.Count - 1].position;

        segments.Add(newSegment);
        newSegment.name = segments.IndexOf(newSegment).ToString();
    }
}
