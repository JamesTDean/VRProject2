using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportationHandler : MonoBehaviourPunCallbacks
{
    private List<Transform> spawnLocations = new List<Transform>(); // Dynamically filled based on tag
    public InputAction leftTriggerAction;
    public GameObject ball; // Reference to the ball object

    private new void OnEnable()
    {
        base.OnEnable(); // It's good practice, though likely not needed here as per Photon's default implementation.
        leftTriggerAction.Enable();
        leftTriggerAction.performed += _ => AttemptTeleport();
    }

    private new void OnDisable()
    {
        base.OnDisable();
        leftTriggerAction.Disable();
        leftTriggerAction.performed -= _ => AttemptTeleport();
    }

    void Start()
    {
        // Initialize spawn locations
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnLocation");
        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnLocations.Add(spawnPoint.transform);
        }

        if (spawnLocations.Count == 0)
        {
            Debug.LogWarning("No spawn locations found with the 'SpawnLocation' tag.");
        }

        // Optionally, find the ball object by tag if not set in the editor
        if (!ball)
        {
            ball = GameObject.FindGameObjectWithTag("BallTag"); // Make sure your ball object has the correct tag
        }
    }

    void AttemptTeleport()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("TeleportPlayerToNearestLocation", RpcTarget.All);
        }
    }

    [PunRPC]
    void TeleportPlayerToNearestLocation()
    {
        if (!ball)
        {
            Debug.LogError("Ball object is not set.");
            return;
        }

        Transform nearestSpawn = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 ballPosition = ball.transform.position; // Use the ball's position

        foreach (Transform spawnLocation in spawnLocations)
        {
            float distance = Vector3.Distance(ballPosition, spawnLocation.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestSpawn = spawnLocation;
            }
        }

        if (nearestSpawn != null)
        {
            this.transform.position = nearestSpawn.position; // Teleport player to the nearest spawn to the ball
        }
    }
}