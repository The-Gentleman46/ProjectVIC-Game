using UnityEngine;
using UnityEngine.InputSystem;

public class snakeMovement : MonoBehaviour
{
    private float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * speed;
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
            
        }
        else if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Collided with food");
            
        }
    }
}
