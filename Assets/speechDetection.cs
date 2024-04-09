using System.Collections;
using System.Collections.Generic;
using Meta.WitAi;
using UnityEngine;
using TMPro;
using Oculus.Voice;
using UnityEngine.XR.Interaction.Toolkit;

public class speechDetection : MonoBehaviour
{
    public AppVoiceExperience wit;
    public TextMeshProUGUI textmesh;

    public XRRayInteractor rayInteractor;
    public TextMeshProUGUI debugText;

    public GameObject location1;
    public GameObject location2;
    public GameObject location3;
    private Ball ball;
    public bool voiceActivated;


    // Start is called before the first frame update
    void Start()
    {

        voiceActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRayInteractorPointingAtBall())
        {
            wit.Activate();
            

        }
    }


    public void transcription(string[] value)
    {

        //textmesh.SetText("value: " + value[0]);
        if (value[0].Equals("restart") || value[0].Equals("Restart"))
        {
            debugText.SetText("restart!!!");
            voiceActivated = true;
            

            //ball.respawn(location1, debugText);
        }
    }

    bool IsRayInteractorPointingAtBall()
    {
        // Check if the Ray Interactor has a valid hit
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            debugText.SetText("hitting sth");

            if (hit.collider.gameObject.tag == "BallTag")
            {


                debugText.SetText("hitting ball");
                Ball ball = hit.collider.gameObject.GetComponent<Ball>();
                if (ball != null && voiceActivated)
                {
                    textmesh.SetText("ball respawn!");
                    ball.respawn(location1);
                    voiceActivated = false;
                }

                return true; // Ray Interactor is pointing at the ball
            }
        }
        return false; // Ray Interactor is not pointing at the ball
    }


}
