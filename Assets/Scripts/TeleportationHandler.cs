using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using TMPro;

public class TeleportationHandler : MonoBehaviourPunCallbacks
{
    private List<Transform> spawnLocations = new List<Transform>(); // Dynamically filled based on tag
    //public InputAction leftTriggerAction;
    public GameObject ball; // Reference to the ball object

    private PlayerManager myPlayerManager;
    private InputData inputData;
    private TextMeshProUGUI debugText;

    public bool test;

    /*
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
    */

    void Start()
    {
        GameObject leftHand = transform.Find("LeftHand").gameObject;
        debugText = leftHand.transform.Find("Canvas").Find("DebugText").GetComponent<TextMeshProUGUI>();
        GameObject myXROrigin = GameObject.Find("XR Origin (XR Rig)");
        inputData = myXROrigin.GetComponent<InputData>();

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

        myPlayerManager = GetComponent<PlayerManager>();

        // Optionally, find the ball object by tag if not set in the editor
        if (!ball)
        {
            ball = myPlayerManager.currentBall;
        }
    }

    void Update()
    {
        if (inputData.rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out float triggerValue))
        {
            if (triggerValue > 0.5f )
            {
                debugText.SetText("Trigger Pressed");
                AttemptTeleport();
            }
            else
            {
                debugText.SetText("No Input");
            }
        }

        if (test)
        {
            AttemptTeleport();
            test = false;
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
        ball = myPlayerManager.currentBall;

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