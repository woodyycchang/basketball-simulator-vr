using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class BoostThrowForce : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [Header("Target")]
    [Tooltip("Empty transform positioned dead-centre of the rim.")]
    public Transform hoopCenter;

    [Header("Arc Settings")]
    [Range(30f, 60f)]
    [Tooltip("Elevation angle of the shot (deg). 45–50 feels like NBA.")]
    public float launchAngleDeg = 48f;

    [Header("Drop Filter")]
    [Tooltip("Releases slower than this (m/s) OR with downward velocity drop naturally.")]
    public float minThrowSpeed = 0.15f;

    Rigidbody _rb;
    float     g;              // +|gravity|

    protected override void Awake()
    {
        base.Awake();
        _rb           = GetComponent<Rigidbody>();
        throwOnDetach = false;                 // we’ll set velocity ourselves
        g             = -Physics.gravity.y;    // e.g. 9.81
    }

    // called when you let go
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);             // XR sets raw release velocity

        Vector3 rawV = _rb.linearVelocity;           // XR’s “real” throw
        if (rawV.magnitude < minThrowSpeed || rawV.y < 0f)
            return;                            // treat as a plain drop

        //------------- calculate 3-D launch for fixed angle -------------
        Vector3 origin      = args.interactorObject.GetAttachTransform(this).position;
        Vector3 target      = hoopCenter.position;
        Vector3 toTarget    = target - origin;

        Vector3 toTargetXZ  = new Vector3(toTarget.x, 0f, toTarget.z);
        float   distXZ      = toTargetXZ.magnitude;      // horizontal distance
        float   heightDiff  = toTarget.y;                // hoop - release height

        float   angleRad    = launchAngleDeg * Mathf.Deg2Rad;
        float   cos         = Mathf.Cos(angleRad);
        float   sin         = Mathf.Sin(angleRad);

        // ensure chosen angle is high enough for this height difference
        float minAngle = Mathf.Atan2(heightDiff, distXZ) + 0.01f;
        if (angleRad < minAngle)
        {
            angleRad = minAngle;                         // bump up slightly
            cos = Mathf.Cos(angleRad);
            sin = Mathf.Sin(angleRad);
        }

        // v² = g d² / (2 cos² (d tanθ – h))
        float inner = distXZ * Mathf.Tan(angleRad) - heightDiff;
        if (inner <= 0f) return;                         // impossible at this angle

        float velSq = g * distXZ * distXZ / (2f * cos * cos * inner);
        if (velSq <= 0f) return;                         // numerical guard
        float v = Mathf.Sqrt(velSq);

        Vector3 launchVel = toTargetXZ.normalized * (v * cos) + Vector3.up * (v * sin);

        //------------- fire the ball -------------
        _rb.isKinematic     = false;
        _rb.useGravity      = true;
        _rb.linearVelocity        = launchVel;
    }
}
