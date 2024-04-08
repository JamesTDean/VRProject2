using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    

    private Rigidbody rb;
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //private void Update()
    //{
    //    float velocity = rb.velocity.magnitude;
    //    if (velocity != 0)
    //    {





    //        rb.velocity *= Mathf.Clamp01(1f - rb.drag * Time.deltaTime);






    //    }


    //}

    //private void FixedUpdate()
    //{
    //    ApplyGravity();
    //}

    //void ApplyGravity() // called during FixedUpdate
    //{
    //    float vel = rb.velocity.y * rb.drag;
    //    float coeff = (1f - Time.fixedDeltaTime * rb.drag);
    //    float force = ((vel + 9.81f) / coeff) * rb.mass; // gravity = 9.81
    //    rb.AddForce(-Vector3.up * force, ForceMode.Force);
    //}


    void FixedUpdate()
    {
        float airDensity = 1.2f; // kg/m^3
        float ballArea = Mathf.PI * Mathf.Pow(rb.transform.localScale.x / 2, 2);
        Vector3 velocity = rb.velocity;
        float velocityMag = velocity.magnitude;

        // Calculate drag force
        Vector3 dragForce = -0.5f * airDensity * velocityMag * velocityMag * 0.5f* ballArea * velocity.normalized;

        // Calculate lift force (simplified, assuming upwards for illustration)
        Vector3 liftForce = Vector3.up * 0.5f * airDensity * velocityMag * velocityMag * 0.2f * ballArea;

        // Apply the forces
        rb.AddForce((dragForce + liftForce) * 1.5f);
    }









    //void OnCollisionEnter(Collision collision)
    //{

    //    Debug.Log("enter collision");
    //    //if (collision.collider.CompareTag("GolfHead"))
    //    //{
    //    //    PutterController putterController = collision.collider.GetComponent<PutterController>();
    //    //    Debug.Log("triggered");
    //    //    if (putterController != null)
    //    //    {
    //    //        Debug.Log("putter not null");
    //    //        Vector3 putterVelocity = putterController.GetVelocity();

    //    //        // You might want to adjust the multiplier to get a more realistic force for your game
    //    //        ballRigidbody.AddForce(putterVelocity, ForceMode.Impulse);

    //    //        // Optional: Apply angular velocity for spin (assuming a perfect contact)
    //    //        Vector3 angularVelocity = putterController.GetAngularVelocity();
    //    //        ballRigidbody.AddTorque(angularVelocity, ForceMode.Impulse);
    //    //    }
    //    //}
    //    velocity_debug velocity_obj = collision.collider.GetComponent<velocity_debug>();
    //    if (velocity_obj != null)
    //    {
    //        Debug.Log(velocity_obj.getSpeed());
    //    }

    //    if (collision.collider.CompareTag("GolfHead"))
    //    {
    //        Debug.Log("hit head");

    //        Debug.Log("check: " + velocity_obj == null);

    //    }




}

