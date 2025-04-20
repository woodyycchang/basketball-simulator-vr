using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class BoostThrowForce : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [Header("Arc & Target")]
    [Tooltip("Center of the rim (create an empty at ring center).")]
    public Transform hoopCenter;

    [Tooltip("How high above release to peak (meters).")]
    public float apexOffset = 2f;

    [Header("Drop vs Throw")]
    [Tooltip("Releases slower than this (m/s) or with downward velocity are treated as plain drops.")]
    public float minThrowSpeed = 0.1f;

    Rigidbody _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        
        throwOnDetach = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // 1) Let XRToolkit finish its normal detach (applies raw velocity)
        base.OnSelectExited(args);

        // 2) Check if it's a drop or downward release
        Vector3 rawVel = _rb.linearVelocity;
        if (rawVel.magnitude < minThrowSpeed || rawVel.y < 0f)
            return;  // plain drop or downward direction

        // 3) Compute NBA‑style arc to hoopCenter
        Vector3 origin = args.interactorObject.GetAttachTransform(this).position;
        Vector3 target = hoopCenter.position;

        float g      = -Physics.gravity.y;
        float apexY  = origin.y + apexOffset;
        float u      = Mathf.Sqrt(2f * (apexY - origin.y) * g);
        float flight = 2f * u / g;

        Vector3 delta      = target - origin;
        Vector3 horizontal = new Vector3(delta.x, 0f, delta.z) / flight;
        Vector3 launchVel  = horizontal + Vector3.up * u;

        // 4) Apply final launch velocity
        _rb.isKinematic = false;
        _rb.useGravity  = true;
        _rb.linearVelocity    = launchVel;
    }
}