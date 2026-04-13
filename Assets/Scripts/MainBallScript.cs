using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MainBallScript : MonoBehaviour
{
    public bool inGoal;
    bool win;
    public Animator ScoreText;
    void Start()
    {
        win = false;
    }

    void Update()
    {
        ScoreText.SetBool("win", inGoal);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreBox"))
        {
            inGoal = true;
            StartCoroutine(Win());

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreBox"))
        {
            inGoal = false;
            StopCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3.1f);
        win = true;
    }
}
