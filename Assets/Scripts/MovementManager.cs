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
    }

    // Update is called once per frame
    void Update()
    {
        if (myView.IsMine)
        {
            myXRRig.position = body.transform.position;
            leftHand.transform.position = leftController.transform.position;
            rightHand.transform.position = rightController.transform.position;

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
        Vector3 upDirection = body.transform.up;
        Vector3 forward = mainCamera.forward;

        Vector2 inputDir = new Vector2(xInput, yInput);
        if (inputDir != new Vector2(0, 0))
        {
            /*
            Vector3 velocityDirection = (forward - upDirection.normalized * Vector3.Dot(forward, upDirection)).normalized;
            float pushMagnitude = Vector2.SqrMagnitude(inputDir);
            Vector3 verticalVelocity = Vector3.Dot(bodyRB.velocity, upDirection) * upDirection.normalized;
            bodyRB.velocity = movementSpeed * pushMagnitude * velocityDirection + verticalVelocity;
            */
            Vector3 verticalVelocity = Vector3.Dot(bodyRB.velocity, upDirection) * upDirection.normalized;
            Vector3 inputVelocity = new Vector3(xInput * movementSpeed, 0f, yInput * movementSpeed);
            bodyRB.velocity = inputVelocity + verticalVelocity;
        }
        debugText.SetText(inputDir.ToString());
    }
}