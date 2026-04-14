using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MainBallScript : MonoBehaviour
{
    public bool inGoal;
    bool win;
    public Animator ScoreText;

    private float timer = 0f;
    private bool isRunning = false;
    bool startedTimer = false;

    public TMP_Text TimerText, LiveTimerText;

    Rigidbody2D rb;

    Coroutine WinCoroutine;
    void Start()
    {
        win = false;
        rb = GetComponent<Rigidbody2D>();

        if(ScoreText == null)
        {
            ScoreText = GameObject.FindWithTag("countdown").GetComponent<Animator>();
        }
        if(LiveTimerText == null)
        {
            LiveTimerText = GameObject.FindWithTag("timer").GetComponent<TMP_Text>();
        }
        if(TimerText == null)
        {
            TimerText = GameObject.FindWithTag("levelwin").GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        ScoreText.SetBool("win", inGoal);

        if (isRunning && !FindFirstObjectByType<MainBallScript>().inGoal)
        {
            timer += Time.deltaTime;
        }

        if(!startedTimer && Input.GetMouseButtonDown(0))
        {
            StartTimer();
        }

        if (startedTimer)
        {

            rb.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
            LiveTimerText.text = GetFormattedTime();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreBox"))
        {
            inGoal = true;
            WinCoroutine = StartCoroutine(Win());

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreBox"))
        {
            inGoal = false;
            StopCoroutine(WinCoroutine);
        }
    }

    IEnumerator Win()
    {

        TimerText.text = "Time Taken: " + GetFormattedTime();
        yield return new WaitForSeconds(3.1f);
        win = true;
        FindFirstObjectByType<GameManager>().Won();
    }

    public string GetFormattedTime()
    {
        return timer.ToString("F1") + "s";
    }

    public void StopTimer()
    {

        TimerText.text = GetFormattedTime();
        isRunning = false;
    }
    public void StartTimer()
    {
        startedTimer = true;
        isRunning = true;
    }
}
