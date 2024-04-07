using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody ballRigidbody;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float velocity = ballRigidbody.velocity.magnitude;
        if (velocity != 0)
        {
            float normalizedVelocity = Mathf.Clamp01(ballRigidbody.velocity.magnitude / 5);
            ballRigidbody.velocity *= Mathf.Clamp01(1f - ballRigidbody.drag * Time.deltaTime);
        }
    }
}
