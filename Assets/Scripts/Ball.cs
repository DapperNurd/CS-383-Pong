using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int bounceForce = 15;

    Rigidbody2D rb;

    public Vector2 direction;

    public static Ball Instance;

    SpriteRenderer sr;
    TrailRenderer tr;

    public bool isGameOver = false;

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();

        ResetBall();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.TryGetComponent(out Paddle paddle)) {
            direction = ClampTrajectory(paddle.CalculateTrajectory(transform.position));
            rb.velocity = direction * bounceForce;

            sr.color = collision.gameObject.GetComponent<SpriteRenderer>().color;
            tr.startColor = sr.color;
            tr.endColor = sr.color;
        }
    }

    private Vector2 GetRandomTrajectory() {

        int directionAsAngle = Random.Range(-30, 31); // Gets a random number between -30 and 30. We treat this as degrees.

        if (Random.Range(0, 2) == 0) directionAsAngle += 180; // 50% chance it will flip it (add 180 to it)

        // Note that the below equation converts a degree (angle) to a Vector2
        return new Vector2(Mathf.Cos(directionAsAngle * Mathf.Deg2Rad), Mathf.Sin(directionAsAngle * Mathf.Deg2Rad)); ;
    }

    private Vector3 ClampTrajectory(Vector3 trajectory) {
        float maxAngle = 0.2f;
        return new Vector3(trajectory.x, Mathf.Clamp(trajectory.y, -maxAngle, maxAngle), trajectory.z).normalized;
    }

    public void ResetBall() {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;

        StartCoroutine(StartMovement());
    }

    IEnumerator StartMovement() {
        yield return new WaitForSeconds(1);
        direction = GetRandomTrajectory();
        rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
    }
}
