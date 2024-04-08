using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportationHandler : MonoBehaviourPunCallbacks
{
    private List<Transform> spawnLocations = new List<Transform>(); // Dynamically filled based on tag
    public InputAction leftTriggerAction;

    private new void OnEnable()
    {
        base.OnEnable(); // Call the base method if necessary (check Photon's documentation).
        leftTriggerAction.Enable();
        leftTriggerAction.performed += _ => AttemptTeleport();
    }

    // Use the 'new' keyword here as well for consistency.
    private new void OnDisable()
    {
        base.OnDisable(); // Call the base method if necessary.
        leftTriggerAction.Disable();
        leftTriggerAction.performed -= _ => AttemptTeleport();
    }

    void Start()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnLocation");
        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnLocations.Add(spawnPoint.transform);
        }

        if (spawnLocations.Count == 0)
        {
            Debug.LogWarning("No spawn locations found with the 'SpawnLocation' tag.");
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
        Transform nearestSpawn = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 playerPosition = this.transform.position;

        foreach (Transform spawnLocation in spawnLocations)
        {
            float distance = Vector3.Distance(playerPosition, spawnLocation.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestSpawn = spawnLocation;
            }
        }

        if (nearestSpawn != null)
        {
            this.transform.position = nearestSpawn.position;
        }
    }
}