using UnityEngine;
using System.Collections.Generic;

public class CrowdManager : MonoBehaviour
{
    public static CrowdManager Instance;

    public List<Animator> crowdAnimators = new List<Animator>();
    public AudioSource cheerSound;
    public AudioSource failSound;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterCrowd(Animator animator)
    {
        crowdAnimators.Add(animator);
    }

    public void OnScore()
    {
        cheerSound.Play();
        foreach (var anim in crowdAnimators)
            anim.SetTrigger("celebration");
    }

    public void OnMiss()
    {
        failSound.Play();
        foreach (var anim in crowdAnimators)
            anim.SetTrigger("celebration2");
    }
}
