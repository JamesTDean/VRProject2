using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public List<int> strokes = new List<int>();
    public List<bool> holeFinished = new List<bool>();
    private TextMeshProUGUI debugText;
    private NetworkPlayerSpawner myNetworkPlayerSpawner;

    public int currentHoleIndex;
    public GameObject currentBall;

    // Start is called before the first frame update
    void Start()
    {
        GameObject leftHand = transform.Find("LeftHand").gameObject;
        debugText = leftHand.transform.Find("Canvas").Find("DebugText").GetComponent<TextMeshProUGUI>();
        myNetworkPlayerSpawner = GameObject.Find("NetworkManager").GetComponent<NetworkPlayerSpawner>();
        currentHoleIndex = 0;
        currentBall = myNetworkPlayerSpawner.spawnedBalls[currentHoleIndex];
        strokes = new List<int> { 0, 0, 0 };
        holeFinished = new List<bool> { false, false, false };
    }

    void Update()
    {
        //debugText.SetText(strokes.ToString());
    }

    public void AddStroke()
    {
        if (!holeFinished[currentHoleIndex])
        {
            strokes[currentHoleIndex] += 1;
        }
        else
        {
            Debug.Log("Hole Finished");
        }
    }

    public void UpdateHole(int holeNumber)
    {
        currentHoleIndex = holeNumber;
        currentBall = myNetworkPlayerSpawner.spawnedBalls[currentHoleIndex];
    }
}
