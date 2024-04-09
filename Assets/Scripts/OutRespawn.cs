using UnityEngine;
using Photon.Pun;
using System;

public class OutRespawn : MonoBehaviourPunCallbacks
{
    private NetworkPlayerSpawner myNetworkPlayerSpawner;
    private PlayerManager manager;
    private int index;
    private int hole;

    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with is the plane and the collision is detected by the ball owner
        if (collision.gameObject.tag == "RespawnPlane" && photonView.IsMine)
        {
            Debug.Log(gameObject);
            // Call the RPC on all clients to move the ball to the respawn position
            photonView.RPC("RespawnBall", RpcTarget.All);
        }
    }
    [PunRPC]
    void RespawnBall()
    {
        try
        {
            // Use a lambda expression for the predicate to match the ball by comparing their PhotonView IDs.
            index = myNetworkPlayerSpawner.spawnedBalls.FindIndex(ball => gameObject);

            if (gameObject != null && index != -1)
            {
                // An array of tags corresponding to each respawn position for indices 0, 1, and 2
                string[] respawnTags = new string[] { "OutSpawn1", "OutSpawn2", "OutSpawn3" };

                // Check if the index is within the bounds of the respawnTags array to avoid IndexOutOfRangeException
                if (index >= 0 && index < respawnTags.Length)
                {
                    // Find the respawn location GameObject by its tag
                    GameObject respawnLocation = GameObject.FindGameObjectWithTag(respawnTags[index]);
                    if (respawnLocation != null)
                    {
                        // Set the ball's position to the found respawn location's position
                        gameObject.transform.position = respawnLocation.transform.position;
                    }
                    else
                    {
                        Debug.LogError("Respawn location with tag " + respawnTags[index] + " not found.");
                    }
                }
                else
                {
                    Debug.LogError("Index out of bounds for respawn tags array.");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error respawning ball: " + ex.Message);
        }
    }
}