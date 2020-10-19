#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameContoller : MonoBehaviour
{

    [SerializeField] private GameObject targetsController;


    [Header("Player Canvas")]
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject gun;
    [SerializeField] private Text countDown;
    [SerializeField] private Text actualGameScore;
    [SerializeField] private Text m_Timer;
    [SerializeField] private AudioSource countDownSound;
    [SerializeField] private AudioSource lastSecSound;

    [SerializeField] private Text fps;


    [Header("Game Over")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text GameOverScoreText;
    [SerializeField] private Text totalScoreGameOver;
    [SerializeField] private AudioSource gameOverSound;


    [Header("End Level Succesfully")]
    [SerializeField] private GameObject EndLevelSuccesfullyCanvas;
    [SerializeField] private Text NextLevelScoreText;
    [SerializeField] private Text totalScoreWin;
    [SerializeField] private AudioSource EndLevelSuccesfullySound;


    [Header("Level Data")]
    [SerializeField] private int minScoreToPass = 150;
    [SerializeField] private int timerForLevel = 30;

    //For FSP
    private int FramCounter = 0;
    private float timeSum = 0;

    //for timer
    private bool startTicks = true;

    private int score = 0;

    //for calculations
    private float m_startTime = 0;
    private int timeleft = 1;

    private bool isGameOver = false;


    void Start()
    {

        StartCoroutine(startCountDown());
    }

    IEnumerator startCountDown()
    {
        yield return new WaitForSeconds(4f);
        playerCanvas.SetActive(true);
        countDownSound.Play();
        m_startTime = Time.fixedTime;
        StartCoroutine(countDowner(2));

    }

    void Update()
    {
        //FSP
        timeSum += Time.deltaTime;
        FramCounter++;
        if (FramCounter > 7)
        {
            fps.text = " FSP: " + (1 / (timeSum / FramCounter));
            timeSum = 0;
            FramCounter = 0;
        }


        if (!isGameOver)
        {
            if (timeleft == 0)
            {
                gun.GetComponent<GunController>().gameStart = false;
                EndLevel(false);
                return;
            }
            if (countDown == null)
            {
                timeleft = (int)(timerForLevel - (((int)((Time.fixedTime - m_startTime) * 100))) / 100f);
                m_Timer.text = timeleft.ToString();
            }
            if (timeleft == 6 && startTicks)
            {
                lastSecSound.Play();
                startTicks = false;
            }
        }

    }
    IEnumerator countDowner(int n)
    {
        if (n > 0)
        {
            yield return new WaitForSeconds(1f);
            countDown.text = "" + n;
            StartCoroutine(countDowner(n - 1));
        }
        else if (n == 0)
        {
            yield return new WaitForSeconds(1f);
            countDown.text = "GO!";
            StartCoroutine(countDowner(n - 1));
        }
        else
        {
            //handle start game staff
            targetsController.SetActive(true);
            yield return new WaitForSeconds(1f);
            m_startTime = Time.fixedTime;
            Destroy(countDown.gameObject);
            aim.SetActive(true);
            gun.SetActive(true);
            gun.GetComponent<GunController>().gameStart = true;

        }
    }



    public void EndLevel(bool isBomb)
    {
        //Handle level end staff
        int actualScore;
        isGameOver = true;
        gun.SetActive(false);
        lastSecSound.Stop();
        playerCanvas.SetActive(false);

        int.TryParse(actualGameScore.text, out actualScore);
        targetsController.SetActive(false);
        StaticObjectScript.totalScore += actualScore;

        //if we get here bot beacuse the bomb explide so we end it succesfully
        if (actualScore >= minScoreToPass && !isBomb) EndLevelSuccesfully();
        else
        {
            GameOver();
        }

        m_Timer.text = "";
        actualGameScore.text = "";
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        gameOverSound.Play();
        GameOverScoreText.text = "Level Score: " + actualGameScore.text;
        totalScoreGameOver.text = "Total Score: " + StaticObjectScript.totalScore;

        StaticObjectScript.totalScore = 0;

    }
    private void EndLevelSuccesfully()
    {
        EndLevelSuccesfullyCanvas.SetActive(true);
        EndLevelSuccesfullySound.Play();
        NextLevelScoreText.text = "Level Score: " + actualGameScore.text;
        totalScoreWin.text = "Total Score: " + StaticObjectScript.totalScore;
    }

    public void AddScore()
    {
        score += 10;
        actualGameScore.text = score.ToString();
    }
}