using UnityEngine;

public class BasketScore : MonoBehaviour
{
    public int score = 0;
    public string ballTag = "Basketball";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ballTag))
        {
            score++;
        }
    }
}
