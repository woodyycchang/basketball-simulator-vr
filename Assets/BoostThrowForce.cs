using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class BoostThrowForce : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [Header("Faster Moderate Throw Settings")]
    [Tooltip("Direct velocity magnitude applied on release (in m/s).")]
    public float moderateVelocity = 8f;

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // Let XR handle the normal detach process.
        base.OnSelectExited(args);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb)
            return;

        if (args.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
        {
            Transform handTransform = interactor.GetAttachTransform(this);
            Vector3 direction = handTransform.forward.normalized;

            rb.linearVelocity = direction * moderateVelocity;

            rb.useGravity = true;
            rb.isKinematic = false;

            rb.linearDamping = 0f;
            rb.angularDamping = 0f;
            rb.angularVelocity = Vector3.zero;

            Debug.Log($"[BoostThrowForce] Applied faster moderate velocity: {direction * moderateVelocity}");
            Debug.DrawRay(transform.position, rb.linearVelocity, Color.red, 2f);
        }
    }
}