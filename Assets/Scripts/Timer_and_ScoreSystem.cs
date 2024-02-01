using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Timer_and_ScoreSystem : MonoBehaviour
{
    public int score = 0;
    private float timeLeft = 150f;
    public Text timerText;
    public Text scoreText;
    // Start is called before the first frame update

    void Start()
    {
        DataManagement.data.LoadData(); 
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            DisplayTime();
        }
    }

    void DisplayTime()
    {
        timerText.text = $"Time: {timeLeft.ToString("0")}";
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        // count the score here: 
        if (trig.CompareTag("EndLevel"))
        {
            score = CountScoreWhenReachEnd();
            scoreText.text = $"Score: {score}";
        }

        if (trig.CompareTag("Coin"))
        {
            score = CountScoreCoin();
            scoreText.text = $"Score: {score}";
            Destroy(trig.gameObject);
        }
    }

    int CountScoreWhenReachEnd()
    {
        // Data is displayed when we reach the end: 
        // Debug.Log("Data says high score is currently" + DataManagement.data.highScore);
        score += (int)timeLeft * 10;
        DataManagement.data.highScore += score; 
        DataManagement.data.SaveData();
        //Debug.Log(DataManagement.data.highScore); 
        Debug.Log("Now that we have added the score to the DataManagement data says high score is currently: " + DataManagement.data.highScore); 
        return score;
    }

    int CountScoreCoin()
    {
        score += 10;
        return score;
    }
}