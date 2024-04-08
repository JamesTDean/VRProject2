using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerObject;
    private GameObject spawnedGolfBall;

    void Start()
    {

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerObject = PhotonNetwork.Instantiate("NetworkPlayer", transform.position, transform.rotation);
        spawnedGolfBall = PhotonNetwork.Instantiate("BallSet", transform.position, transform.rotation);
        spawnedGolfBall.transform.position = new Vector3(1.5f, -1f, 3f);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerObject);
        PhotonNetwork.Destroy(spawnedGolfBall);
    }
}
