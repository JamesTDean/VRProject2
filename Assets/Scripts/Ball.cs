using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    private float cooldownTime;
    private PlayerManager myPlayerManagerScript;
    private bool isCooldown;

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
}
