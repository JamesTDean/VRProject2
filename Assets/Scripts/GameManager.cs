using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu;
    private TextMeshProUGUI currentHole;
    private TextMeshProUGUI strokesHole;
    private TextMeshProUGUI strokesTotal;
    private TextMeshProUGUI holeOver;

    private PlayerManager myPlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        uiMenu = GameObject.Find("UI");
        currentHole = uiMenu.transform.Find("Hole").GetComponent<TextMeshProUGUI>();
        strokesHole = uiMenu.transform.Find("Strokes").GetComponent<TextMeshProUGUI>();
        strokesTotal = uiMenu.transform.Find("Total Score").GetComponent<TextMeshProUGUI>();
        holeOver = uiMenu.transform.Find("Hole Over").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayerManager != null)
        {
            if (!myPlayerManager.holeFinished[myPlayerManager.currentHoleIndex])
            {
                currentHole.gameObject.SetActive(true);
                strokesHole.gameObject.SetActive(true);
                strokesTotal.gameObject.SetActive(true);
                holeOver.gameObject.SetActive(false);

                int holeNum = myPlayerManager.currentHoleIndex + 1;
                currentHole.SetText("Current Hole: " + holeNum.ToString());

                int totalStrokes = 0;

                foreach(var strokeCount in myPlayerManager.strokes)
                {
                    totalStrokes += strokeCount;
                }

                //change this later
                strokesHole.SetText("Strokes (Hole): " + myPlayerManager.strokes[myPlayerManager.currentHoleIndex].ToString());
                strokesTotal.SetText("Strokes (Total): " + totalStrokes.ToString());
            }
            else
            {
                currentHole.gameObject.SetActive(false);
                strokesHole.gameObject.SetActive(false);
                strokesTotal.gameObject.SetActive(false);
                holeOver.gameObject.SetActive(true);
            }
        }
        else
        {
            try
            {
                var photonViews = FindObjectsOfType<PhotonView>();
                foreach (var view in photonViews)
                {
                    if (view.gameObject.name == "NetworkPlayer(Clone)")
                    {
                        if (view.IsMine)
                        {
                            myPlayerManager = view.gameObject.GetComponent<PlayerManager>();
                        }

                    }
                }
            }
            catch
            {
                Debug.Log("Player not in Scene");
            }
        }
    }
}
