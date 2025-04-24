using UnityEngine;

public class CrowdMember : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        CrowdManager.Instance.RegisterCrowd(animator);
    }
}
