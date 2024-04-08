using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTeleportation : MonoBehaviour
{
    // Use serialized fields to assign these in the Inspector.
    [SerializeField] private GameObject[] holes;
    [SerializeField] private GameObject[] destinations;

    private void Start()
    {
        // Verify that each hole has a corresponding destination
        if (holes.Length != destinations.Length)
        {
            Debug.LogError("Holes and destinations count do not match.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < holes.Length; i++)
        {
            // Check if the colliding object is one of the holes
            if (other.gameObject == holes[i])
            {
                // Preserve the current velocity
                Vector3 velocity = GetComponent<Rigidbody>().velocity;

                // Teleport the ball to the corresponding destination
                transform.position = destinations[i].transform.position;

                // Reapply the velocity
                GetComponent<Rigidbody>().velocity = velocity;

                // Exit the loop once the correct hole is found and processed
                break;
            }
        }
    }
}
