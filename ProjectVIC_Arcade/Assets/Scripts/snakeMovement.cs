using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class snakeMovement : MonoBehaviour
{
    [SerializeField] private float rotationOffset;
    [SerializeField] private spawnFood spawnFoodScript;

    private float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput * speed;
        
        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)) {
            direction = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow)) {
            direction = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)) {
            direction = Vector3.right;
        }

        if (direction == Vector3.forward)
        {
            rotationOffset = 0f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        else if (direction == Vector3.back)
        {
            rotationOffset = 180f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        else if (direction == Vector3.left)
        {
            rotationOffset = 90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        else if (direction == Vector3.right)
        {
            rotationOffset = -90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveInput = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveInput = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveInput = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveInput = Vector2.right;
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
        else if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Collided with food");
            Destroy(collision.gameObject);
            gameHandler.instance.addLength();
            gameHandler.instance.addScore();
            spawnFoodScript.numberOfSpawned--;
        }
    }


    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        Debug.Log("Collided with wall");
    //        Destroy(gameObject);
    //        End();
    //    }
    //    else if (collision.gameObject.CompareTag("Food"))
    //    {
    //        Debug.Log("Collided with food");
    //        Destroy(collision.gameObject);
    //        gameHandler.instance.addLength();
    //        gameHandler.instance.addScore();
    //    }
    //}

    
}
