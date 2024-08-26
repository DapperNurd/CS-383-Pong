using UnityEngine;

public enum MoveType {
    Player1Controlled,
    Player2Controlled,
    AIControlled,
}

public class Paddle : MonoBehaviour
{
    public MoveType moveType;
    private Vector2 movement;
    private float movementSpeed = 5f;

    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        switch (moveType) {
            case MoveType.Player1Controlled:
                movement = new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"));
                break;
            case MoveType.Player2Controlled:
                movement = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
                break;
            case MoveType.AIControlled:
                // We track the Ball's position by using the Singleton behavior. If we wanted more balls per scene, this would get more complicated and need a rework.
                
                // Here we want the enemy to follow the ball if it's active, otherwise go back to the starting point (0).
                float targetPos = Ball.Instance.gameObject.activeSelf ? Ball.Instance.transform.position.y : 0;

                if (targetPos - transform.position.y > 0.45f) movement = Vector2.up;
                else if (targetPos - transform.position.y < -0.45f) movement = Vector2.down;
                else movement = Vector2.zero;
                break;
            default: break;
        }
    }

    private void FixedUpdate() {
        rb.velocity = movement * movementSpeed;
    }

    public Vector2 CalculateTrajectory(Vector3 offset) {
        return (offset - transform.position).normalized;
    }
}
