using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTeleportation : MonoBehaviour
{
    // Use serialized fields to assign these in the Inspector.
    [SerializeField] private List<GameObject> holes = new List<GameObject>();
    [SerializeField] private List<GameObject> destinations = new List<GameObject>();

    private void Start()
    {
        GameObject holesHolder = GameObject.Find("Course3");

        for(int i = 0; i < 6; i++)
        {
            GameObject child = holesHolder.transform.GetChild(i).gameObject;
            if (i < 3)
            {
                holes.Add(child);
            }
            else
            {
                destinations.Add(child);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < 3; i++)
        {
            // Check if the colliding object is one of the holes
            if (other.gameObject == holes[i])
            {
                // Preserve the current velocity
                Vector3 velocity = GetComponent<Rigidbody>().velocity;

                // Teleport the ball to the corresponding destination
                transform.position = destinations[i].transform.position;

                // Reapply the velocity
                GetComponent<Rigidbody>().velocity = new Vector3(-velocity.x, velocity.y, velocity.z);

                // Exit the loop once the correct hole is found and processed
                break;
            }
        }
    }
}
