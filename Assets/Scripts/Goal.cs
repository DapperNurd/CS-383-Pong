using System.Collections;
using TMPro;
using UnityEngine;

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
        if (score) score.Value++;
        if (text) text.text = score.Value.ToString("n0");

        StartCoroutine(OnGoal(collision.GetComponent<Ball>()));
    }

    IEnumerator OnGoal(Ball ball) {
        ball.gameObject.SetActive(false);

        Debug.Log(sr.color);

        var main = explosion.main;
        main.startColor = sr.color;

        explosion.transform.position = new Vector2(transform.position.x, ball.transform.position.y);
        explosion.Play();

        yield return new WaitForSeconds(1f);

        ball.gameObject.SetActive(true);
        ball.ResetBall();
    }
}
