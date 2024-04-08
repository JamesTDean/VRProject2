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


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRayInteractorPointingAtBall())
        {
            wit.Activate();
            debugText.SetText("activated!");
            
        }
    }


    public void transcription(string[] value)
    {

        textmesh.SetText("value: "+  value[0]);
        if (value[0].Equals("restart") || value[0].Equals("Restart"))
        {
            textmesh.SetText("changed!!!!!!!");
            debugText.SetText("restart, ball respawn!" + value[0]);
        }
    }

    bool IsRayInteractorPointingAtBall()
    {
        // Check if the Ray Interactor has a valid hit
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            debugText.SetText("hitting sth");

            if (hit.collider.gameObject.tag == "Ball")
            {

                Debug.Log("hitting ball");
                debugText.SetText("hitting ball");
                return true; // Ray Interactor is pointing at the ball
            }
        }
        return false; // Ray Interactor is not pointing at the ball
    }
}