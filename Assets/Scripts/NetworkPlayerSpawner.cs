using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject locations;
    private GameObject spawnedPlayerObject;
    public List<GameObject> spawnedBalls = new List<GameObject>();
    public List<GameObject> spawnedClubs = new List<GameObject>();

    void Start()
    {

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        for (int i = 1; i < 4; i++)
        {
            string locationString = "Spawn" + i.ToString();
            Transform location = locations.transform.Find(locationString);
            GameObject spawnedGolfBall = PhotonNetwork.Instantiate("BallSet", location.position, location.rotation);
            spawnedBalls.Add(spawnedGolfBall);
            PhotonView ballView = spawnedGolfBall.GetComponent<PhotonView>();
            Vector3 clubPosition = location.position + new Vector3(0f, 1f, 1.5f);
            GameObject spawnedGolfClub = PhotonNetwork.Instantiate("GolfClubEmpty", clubPosition, location.rotation);
            spawnedClubs.Add(spawnedGolfClub);
        }
        spawnedPlayerObject = PhotonNetwork.Instantiate("NetworkPlayer", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerObject);
        foreach (var ball in spawnedBalls)
        {
            //spawnedBalls.Remove(ball);
            PhotonNetwork.Destroy(ball);
        }
        spawnedBalls = new List<GameObject>();
        foreach (var club in spawnedClubs)
        {
            //spawnedClubs.Remove(club);
            PhotonNetwork.Destroy(club);
        }
        spawnedClubs = new List<GameObject>();
    }
}
