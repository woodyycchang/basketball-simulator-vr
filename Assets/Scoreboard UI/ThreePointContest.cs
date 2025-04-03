using UnityEngine;
using TMPro;

public class ThreePointContest : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Displays the score
    public TextMeshProUGUI timerText;  // Displays the timer

    private int score = 0;
    private float timeLeft = 60f; // 60 seconds countdown
    private bool isGameActive = true;
    private int shotsTaken = 0;
    private int maxShots = 25;

    // Money Ball positions (e.g., 4th, 9th, 14th, 19th, 24th shot)
    private int[] moneyBallPositions = {4, 9, 14, 19, 24};

    void Start()
    {
        UpdateScoreboard();
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Simulate a shot
        {
            SimulateShotMade();
        }
    }

    // Simulate shot made
    void SimulateShotMade()
    {
        if (!isGameActive || shotsTaken >= maxShots) return;

        bool isMoneyBall = System.Array.Exists(moneyBallPositions, pos => pos == shotsTaken);
        int points = isMoneyBall ? 2 : 1;

        score += points;
        shotsTaken++;

        Debug.Log(isMoneyBall ? "Money Ball! +2 points" : "Regular Shot! +1 point");

        UpdateScoreboard();
    }

    // Update the timer (with tenths of a second) and scoreboard
    void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateScoreboard();
        }
        else
        {
            isGameActive = false;
            timerText.text = "Time: 0.0";
            Debug.Log("Game Over!");
        }
    }

    // Update score and timer display
    void UpdateScoreboard()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();;

        if (timerText != null)
            timerText.text = timeLeft.ToString("F1"); 
    }
}
