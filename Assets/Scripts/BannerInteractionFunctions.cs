using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class BannerInteractionFunctions : MonoBehaviour
{
    private TextMeshProUGUI debugText;
    private Transform playerTransform;
    private TeleportationHandler myTeleportationHandler;

    // Start is called before the first frame update
    void Start()
    {
        debugText = null;
        playerTransform = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform == null)
        {
            var photonViews = FindObjectsOfType<PhotonView>();
            foreach (var view in photonViews)
            {
                if (view.gameObject.name == "NetworkPlayer(Clone)")
                {
                    if (view.IsMine)
                    {
                        playerTransform = view.transform;
                        GameObject leftHand = playerTransform.Find("LeftHand").gameObject;
                        debugText = leftHand.transform.Find("Canvas").Find("DebugText").GetComponent<TextMeshProUGUI>();
                        myTeleportationHandler = playerTransform.GetComponent<TeleportationHandler>();
                    }

                }
            }
        }
        
        if(playerTransform != null)
        {
            transform.LookAt(playerTransform.Find("Body"));
            //transform.rotation *= Quaternion.Euler(0, 180, 0);
        }
    }

    public void ChangeText(string text)
    {
        debugText.SetText(text);
    }

    public void MovePlayer(int holeNum)
    {
        myTeleportationHandler.MoveHoles(holeNum-1);
    }
}
