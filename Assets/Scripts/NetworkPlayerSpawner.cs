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

    Color[] colors;

    void Start()
    {
        colors = new Color[3];
        colors[0] = new Color(0, 0, 1f);
        colors[1] = new Color(0.5f, 0f, 0.5f);
        colors[2] = new Color(1f, 0, 0);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        for (int i = 1; i < 4; i++)
        {
            string locationString = "Spawn" + i.ToString();
            Transform location = locations.transform.Find(locationString);
            GameObject spawnedGolfBall = PhotonNetwork.Instantiate("BallSet", location.position, location.rotation);

            Transform ballTransform = spawnedGolfBall.transform.GetChild(1);
            Ball ball = ballTransform.gameObject.GetComponent<Ball>();
            ball.course = i;

            // change color
            ballTransform.gameObject.GetComponent<Renderer>().material.color = colors[i - 1];


            spawnedBalls.Add(spawnedGolfBall);
            PhotonView ballView = spawnedGolfBall.GetComponent<PhotonView>();
            string golfClubString = "GolfClubInteractable";
            if (ballView.IsMine)
            {
                golfClubString = "myGolfClubInteractable";
            }
            Vector3 clubPosition = location.position + new Vector3(0f, 1f, 1.5f);
            GameObject spawnedGolfClub = PhotonNetwork.Instantiate(golfClubString, clubPosition, location.rotation);
            spawnedGolfClub.GetComponent<Renderer>().material.color = colors[i - 1];
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
