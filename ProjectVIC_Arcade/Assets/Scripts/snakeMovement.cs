using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class snakeMovement : MonoBehaviour
{
    [SerializeField] private float rotationOffset;


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
        
        if (direction == Vector3.forward) { 
            rotationOffset = 0f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            //transform.eulerAngles = new Vector3(0f, 0f, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        else if (direction == Vector3.back) {
            rotationOffset = 180f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            //transform.eulerAngles = new Vector3(0f, 0f, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        else if (direction == Vector3.left) {
            rotationOffset = 90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            //transform.eulerAngles = new Vector3(0f, 0f, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
        else if (direction == Vector3.right) {
            rotationOffset = -90f;
            transform.rotation = Quaternion.Euler(0, 0, rotationOffset);
            //transform.eulerAngles = new Vector3(0f, 0f, rotationOffset);
            rb.MoveRotation(rotationOffset);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
    }  

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collided with wall");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Collided with food");
            Destroy(collision.gameObject);
        }
    }

    public void End()
    {
        SceneManager.LoadScene("gameOver");
    }
}
