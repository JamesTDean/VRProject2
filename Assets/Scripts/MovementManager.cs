using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using TMPro;

public class MovementManager : MonoBehaviour
{
    private PhotonView myView;
    private InputData inputData;

    private Transform myXRRig;
    private Transform mainCamera;
    private GameObject body;
    private Rigidbody bodyRB;
    private GameObject leftHand;
    private GameObject rightHand;
    private GameObject leftController;
    [SerializeField] private GameObject rightController;
    private TextMeshProUGUI debugText;

    private float xInput;
    private float yInput;
    private float movementSpeed = 5f;

    [SerializeField]private NetworkPlayerSpawner myNetworkPlayerSpawner;
    [SerializeField]private List<GameObject> invisiblePutters = new List<GameObject>();
    [SerializeField]private List<GameObject> putterModels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        myView = GetComponent<PhotonView>();

        body = transform.Find("Body").gameObject;
        leftHand = transform.Find("LeftHand").gameObject;
        rightHand = transform.Find("RightHand").gameObject;
        bodyRB = body.GetComponent<Rigidbody>();

        leftController = GameObject.Find("Left Controller");
        rightController = GameObject.Find("Right Controller");

        GameObject myXROrigin = GameObject.Find("XR Origin (XR Rig)");
        myXRRig = myXROrigin.transform;
        inputData = myXROrigin.GetComponent<InputData>();
        mainCamera = myXRRig.Find("Camera Offset").Find("Main Camera");

        debugText = leftHand.transform.Find("Canvas").Find("DebugText").GetComponent<TextMeshProUGUI>();

        myNetworkPlayerSpawner = GameObject.Find("NetworkManager").GetComponent<NetworkPlayerSpawner>();
        putterModels = myNetworkPlayerSpawner.spawnedClubs;
        GameObject interactableClubs = GameObject.Find("UsersClubs");
        foreach (Transform club in interactableClubs.transform)
        {
            invisiblePutters.Add(club.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myView.IsMine)
        {
            myXRRig.transform.parent.position = body.transform.position;
            myXRRig.transform.localPosition = -mainCamera.localPosition + new Vector3(0, 1.2f, 0);

            leftHand.transform.position = leftController.transform.position;
            rightHand.transform.position = rightController.transform.position;

            for(int i = 0; i < 3; i++)
            {
                putterModels[i].transform.position = invisiblePutters[i].transform.Find("VisualAttachment").position;
                putterModels[i].transform.rotation = invisiblePutters[i].transform.Find("VisualAttachment").rotation;
            }

            if (inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
            {
                xInput = movement.x;
                yInput = movement.y;
            }
        }
        if (myView.IsMine)
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if (myView.IsMine)
        {
            Vector3 upDirection = body.transform.up;
            Vector3 forward = mainCamera.forward;

            Vector2 inputDir = new Vector2(xInput, yInput);
            Vector3 lookDirection = (forward - upDirection * Vector3.Dot(forward, upDirection)).normalized;
            Quaternion rotation = Quaternion.AngleAxis(Vector2.Angle(new Vector2(0, 1f), inputDir.normalized), upDirection);
            if (xInput < 0)
            {
                rotation = Quaternion.AngleAxis(-Vector2.Angle(new Vector2(0, 1f), inputDir.normalized), upDirection);
            }
            Vector3 velocityDirection = rotation * lookDirection;
            float pushMagnitude = Vector2.SqrMagnitude(inputDir);
            Vector3 verticalVelocity = new Vector3(0f, bodyRB.velocity.y, 0f);
            bodyRB.velocity = movementSpeed * pushMagnitude * velocityDirection + verticalVelocity;

            //debugText.SetText(inputDir.ToString());
        }
    }
}