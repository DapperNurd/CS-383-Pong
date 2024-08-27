using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] IntObject score;
    [SerializeField] TMP_Text text;

    [SerializeField] ParticleSystem explosion;

    SpriteRenderer sr;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();

        if (score) score.Value = 0;
        if (text) text.text = score.Value.ToString("n0");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // This might be a little backwards, but this is when something enters the goal's trigger, not when the goal enters a trigger

        Ball ball = collision.GetComponent<Ball>();

        if (score) {
            score.Value++;
            if(score.Value >= 5) {
                // Would have liked to do some event based stuff but I was running short on time
                ball.isGameOver = true;

                // This is really ugly but we're setting it based on the scene
                string msg;
                if(SceneManager.GetActiveScene().name.Contains("AI")) {
                    msg = score.name.Contains("1") ? "You win!" : "You lose!";
                }
                else {
                    msg = score.name + " Wins!";
                }
                
                EndMenu endMenu = FindAnyObjectByType<EndMenu>(); // I don't generally use this function
                endMenu.EnableMenu();
                endMenu.SetEndTitle(msg);
            }
        }
        if (text) text.text = score.Value.ToString("n0");

        StartCoroutine(OnGoal(ball));
    }

    IEnumerator OnGoal(Ball ball) {
        ball.gameObject.SetActive(false);

        Debug.Log(sr.color);

        var main = explosion.main;
        main.startColor = sr.color;

        explosion.transform.position = new Vector2(transform.position.x, ball.transform.position.y);
        explosion.Play();

        yield return new WaitForSeconds(1f);

        if(!ball.isGameOver) {
            ball.gameObject.SetActive(true);
            ball.ResetBall();
        }
    }
}
