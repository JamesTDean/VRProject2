using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class confirm : MonoBehaviour
{
    

    public GameObject window;
    public Button confirmButton;
    public Button cancelButton;


    void Start()
    {
        confirmButton.onClick.AddListener(ConfirmAction);
        cancelButton.onClick.AddListener(CancelAction);

        // Optionally, hide the window at start
        window.SetActive(false);
    }

    void ConfirmAction()
    {
        speechDetection speech = GameObject.Find("ResponseHandler").GetComponent<speechDetection>();
        speech.ui = true;
        window.SetActive(false); // Hide the window
        
    }

    void CancelAction()
    {
        window.SetActive(false); // Hide the window
    }

    public void ShowWindow()
    {
        window.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
