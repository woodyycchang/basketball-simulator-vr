using UnityEngine;
using TMPro;

public class BasketScore : MonoBehaviour
{
    public int score = 0;
    public string ballTag = "Basketball";

    public TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ballTag))
        {
            score++;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
