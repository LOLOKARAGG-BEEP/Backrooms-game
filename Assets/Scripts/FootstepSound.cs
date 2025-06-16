using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudio;
    public float baseStepDelay = 0.5f;     // задержка при обычной скорости
    public float maxSpeed = 6f;            // скорость, при которой delay минимальный
    public float minStepDelay = 0.2f;      // минимально возможная задержка

    private Rigidbody rb;
    private float stepTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float speed = horizontalVelocity.magnitude;

        bool isMoving = speed > 0.1f;

        // Линейно уменьшаем delay при увеличении скорости
        float stepDelay = Mathf.Lerp(baseStepDelay, minStepDelay, speed / maxSpeed);

        if (isMoving && IsGrounded())
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepDelay)
            {
                footstepAudio.Play();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepDelay;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
