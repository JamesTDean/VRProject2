//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;

//[RequireComponent(typeof(Rigidbody))]

//public class Ball : MonoBehaviour
//{
//    private Rigidbody ballRigidbody;
//    private float cooldownTime;
//    private PlayerManager myPlayerManagerScript;
//    private bool isCooldown;

//    //GameObject location1;
//    //GameObject location2;
//    //GameObject location3;

//    void Start()
//    {
//        if (transform.GetComponentInParent<PhotonView>().IsMine)
//        {
//            gameObject.layer = 8;
//        }

//        ballRigidbody = GetComponent<Rigidbody>();
//        var photonViews = FindObjectsOfType<PhotonView>();
//        foreach (var view in photonViews)
//        {
//            if (view.gameObject.name == "NetworkPlayer(Clone)")
//            {
//                if (view.IsMine)
//                {
//                    myPlayerManagerScript = view.gameObject.GetComponent<PlayerManager>();
//                }

//            }
//        }
//    }

//    void Update()
//    {
//        //float velocity = ballRigidbody.velocity.magnitude;
//        //if (velocity != 0)
//        //{
//        //    float normalizedVelocity = Mathf.Clamp01(ballRigidbody.velocity.magnitude / 5);
//        //    ballRigidbody.velocity *= Mathf.Clamp01(1f - ballRigidbody.drag * Time.deltaTime);
//        //}
//        if (Time.time > cooldownTime)
//        {
//            isCooldown = false;
//        }
//    }

//    void FixedUpdate()
//    {
//        float airDensity = 1.2f; // kg/m^3
//        float ballArea = Mathf.PI * Mathf.Pow(ballRigidbody.transform.localScale.x / 2, 2);
//        Vector3 velocity = ballRigidbody.velocity;
//        float velocityMag = velocity.magnitude;

//        // Calculate drag force
//        Vector3 dragForce = -0.5f * airDensity * velocityMag * velocityMag * 0.5f * ballArea * velocity.normalized;

//        // Calculate lift force (simplified, assuming upwards for illustration)
//        Vector3 liftForce = Vector3.up * 0.5f * airDensity * velocityMag * velocityMag * 0.2f * ballArea;

//        // Apply the forces
//        ballRigidbody.AddForce((dragForce + liftForce) * 1.5f);
//    }


//    void OnCollisionExit(Collision collision)
//    {
//        Debug.Log(collision.gameObject.name);
//        if (collision.gameObject.tag == "Club" && !isCooldown)
//        {
//            myPlayerManagerScript.AddStroke();
//            isCooldown = true;
//            cooldownTime = Time.time + 0.5f;
//        }
//    }

//    public void respawn(GameObject resetLocation)
//    {
//        this.transform.position = resetLocation.transform.position;
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class Ball : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    private float cooldownTime;
    private PlayerManager myPlayerManagerScript;
    private bool isCooldown;

    public int course;

    //public bool voiceActivated;

    void Start()
    {
      
        if (transform.GetComponentInParent<PhotonView>().IsMine)
        {
            gameObject.layer = 8;
        }

        ballRigidbody = GetComponent<Rigidbody>();
        var photonViews = FindObjectsOfType<PhotonView>();
        foreach (var view in photonViews)
        {
            if (view.gameObject.name == "NetworkPlayer(Clone)")
            {
                if (view.IsMine)
                {
                    myPlayerManagerScript = view.gameObject.GetComponent<PlayerManager>();
                }

            }
        }
        //voiceActivated = false;
        
    }

    void Update()
    {
        float velocity = ballRigidbody.velocity.magnitude;
        if (velocity != 0)
        {
            float normalizedVelocity = Mathf.Clamp01(ballRigidbody.velocity.magnitude / 5);
            ballRigidbody.velocity *= Mathf.Clamp01(1f - ballRigidbody.drag * Time.deltaTime);
        }
        if (Time.time > cooldownTime)
        {
            isCooldown = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Club" && !isCooldown)
        {
            myPlayerManagerScript.AddStroke();
            isCooldown = true;
            cooldownTime = Time.time + 0.5f;
        }
    }
    public void respawn(GameObject resetLocation)
    {
        this.transform.position = resetLocation.transform.position;
        //this.voiceActivated = false;

    }
}