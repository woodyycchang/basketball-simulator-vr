using UnityEngine;

public class AudienceGroup : MonoBehaviour
{
    private Animation[] animations;

    void Awake()
    {
        animations = GetComponentsInChildren<Animation>();
    }

    public void PlayAll()
    {
        foreach (var anim in animations)
        {
            anim.Play();
        }
    }
}
