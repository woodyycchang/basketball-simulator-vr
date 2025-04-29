using UnityEngine;
using TMPro;

public class BasketScore : MonoBehaviour
{
    public int score = 0;
    public string ballTag = "Basketball";
    public TextMeshProUGUI scoreText;

    [Header("Score Sound")]
    public AudioClip scoreClip; 
    private AudioSource audioSrc;

    [Header("Audience Animation")]
    public AudienceGroup audienceGroup;

    void Awake()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.playOnAwake = false;
        audioSrc.volume = 0.5f;
    }

    void Start() => UpdateScoreText();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ballTag))
        {
            score++;
            UpdateScoreText();
            if (scoreClip) audioSrc.PlayOneShot(scoreClip, 0.5f);

            if (audienceGroup)
                audienceGroup.PlayAll();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText) scoreText.text = score.ToString();
    }
}
