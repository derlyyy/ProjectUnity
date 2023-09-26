using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    
    public FixedJoystick joystick;
    private Rigidbody2D rb;
    private Vector2 move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = rb.position + move * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, 0.2f)); // Плавное движение с коэффициентом 0.2f
    }
}