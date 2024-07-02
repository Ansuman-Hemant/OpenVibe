using UnityEngine;

public class TimeSignalMovement : MonoBehaviour
{
    public float speed = 1.0f;  // Speed of the movement
    private float timeSignal;

    void Update()
    {
        // Update time signal (for demonstration, using a sine wave)
        timeSignal = Mathf.Sin(Time.time);

        // Move the GameObject based on the time signal
        if (timeSignal > 0)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}
