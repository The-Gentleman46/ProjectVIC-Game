using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * speed;
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
