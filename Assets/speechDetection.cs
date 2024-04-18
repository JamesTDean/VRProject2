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

    public bool ui;
    bool isHit;

    public GameObject window;
    

    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        voiceActivated = false;
        ui = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRayInteractorPointingAtBall())
        {
            wit.Activate();
            
        }
        check(ball);
    }


    public void transcription(string[] value)
    {

        
        if (value[0].Equals("restart") || value[0].Equals("Restart"))
        {
            
            window.SetActive(true);
            voiceActivated = true;
            
        }
    }

    bool IsRayInteractorPointingAtBall()
    {
        // Check if the Ray Interactor has a valid hit
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider.gameObject.tag == "BallTag")
            {
                ball = hit.collider.gameObject.GetComponent<Ball>();

                isHit = true;
                //debugText.SetText("hitting ball");



                return true; // Ray Interactor is pointing at the ball
            }


            
        }
        return false; // Ray Interactor is not pointing at the ball
    }

    void check(Ball obj)
    {
        
            if (isHit && voiceActivated && ui)
            {

                //debugText.SetText("ball respawn!");
                int course = obj.course;
                if (course == 1)
                {
                    //debugText.SetText("hitting ball1111111: " + ball.course);
                    obj.respawn(location1);
                }
                if (course == 2)
                {
                    //debugText.SetText("hitting ball2");
                    obj.respawn(location2);
                }
                if (course == 3)
                {
                    //debugText.SetText("hitting ball3");
                    obj.respawn(location3);
                }

                voiceActivated = false;
                this.ui = false;
                isHit = false;
            }


        //}
    }


}
