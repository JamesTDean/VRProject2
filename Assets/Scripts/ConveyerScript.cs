using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerScript : MonoBehaviour
{
    public Vector3 forceDirection;
    public float forceMagnitude;

    void Start()
    {
        //forceMagnitude = 5f;
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.ToString());

        if (other.transform.tag == "BallTag")
        {
            Rigidbody ballRB = null;
            try
            {
                ballRB = other.gameObject.GetComponent<Rigidbody>();
            }
            catch
            {
                print("No rigid body component.");
            }
            if (ballRB != null)
            {
                ballRB.AddForce(forceMagnitude * forceDirection);
            }
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.ToString());

        if (other.transform.tag == "Ball")
        {
            Rigidbody ballRB = null;
            try
            {
                ballRB = other.gameObject.GetComponent<Rigidbody>();
            }
            catch
            {
                print("No rigid body component.");
            }
            if (ballRB != null)
            {
                ballRB.AddForce(forceMagnitude * forceDirection);
            }
        }
    }
    */
}
