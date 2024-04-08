using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    private int strokes;
    private TextMeshProUGUI debugText;
    private NetworkPlayerSpawner myNetworkPlayerSpawner;

    private int currentHole;
    public GameObject currentBall;

    // Start is called before the first frame update
    void Start()
    {
        strokes = 0;
        GameObject leftHand = transform.Find("LeftHand").gameObject;
        debugText = leftHand.transform.Find("Canvas").Find("DebugText").GetComponent<TextMeshProUGUI>();
        myNetworkPlayerSpawner = GameObject.Find("NetworkManager").GetComponent<NetworkPlayerSpawner>();
        currentHole = 0;
        currentBall = myNetworkPlayerSpawner.spawnedBalls[currentHole];
    }

    void Update()
    {
        debugText.SetText(strokes.ToString());
    }

    public void AddStroke()
    {
        strokes += 1;
    }

    public void UpdateHole(int holeNumber)
    {
        currentHole = holeNumber;
        currentBall = myNetworkPlayerSpawner.spawnedBalls[currentHole];
    }
}
