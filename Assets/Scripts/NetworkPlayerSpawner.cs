using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerObject;
    private GameObject spawnedGolfBall;

    public GameObject location;

    void Start()
    {

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerObject = PhotonNetwork.Instantiate("NetworkPlayer", transform.position, transform.rotation);
        spawnedGolfBall = PhotonNetwork.Instantiate("BallSet", transform.position, transform.rotation);
        //spawnedGolfBall.transform.position = new Vector3(-4.5f, -1f, 0.5f);
        spawnedGolfBall.transform.position = location.transform.position;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerObject);
        PhotonNetwork.Destroy(spawnedGolfBall);
    }
}
