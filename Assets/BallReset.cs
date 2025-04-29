using UnityEngine;

public class BallReset : MonoBehaviour
{
    public Transform resetPoint;

    public float resetDelay = 30f;
    private bool isOnGround = false;
    private float timer = 0f;

    void Update()
    {
        if (isOnGround)
        {
            timer += Time.deltaTime;
            if (timer >= resetDelay)
            {
                ResetBall();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            timer = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
            timer = 0f;
        }
    }

    private void ResetBall()
    {
        transform.position = resetPoint.position;
        transform.rotation = resetPoint.rotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        isOnGround = false;
        timer = 0f;
    }
}
