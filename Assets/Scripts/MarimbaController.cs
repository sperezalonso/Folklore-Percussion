using System;
using System.Collections;
using System.Collections.Generic;
using OVR.OpenVR;
using UnityEngine;
using UnityEngine.Serialization;

public class MarimbaController : MonoBehaviour
{

    private AudioSource key;
    private AudioClip note;
    private OVRInput.Controller leftController;
    private OVRInput.Controller rightController;

    [SerializeField] private float vibrationFrequency = 0f;
    [SerializeField] private float vibrationAmplitude = 0.1f;

	// Use this for initialization
	void Start ()
	{
	    key = GetComponent<AudioSource>();
	    note = GetComponent<AudioClip>();
	    leftController = OVRInput.Controller.LTouch;
	    rightController = OVRInput.Controller.RTouch;
	}
	
	// Update is called once per frame
	void Update () {
	    //if (Input.GetKeyDown(KeyCode.Alpha1)) speakers[0].GetComponent<AudioSource>().Play();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mallet")
        {   
            CheckMalletMotionDirection(other);
        }
    }

    void CheckMalletMotionDirection(Collider collider)
    {
        // Check if mallet is moving upwards or downwards when hitting the marimba keys
        // Marimba notes should only sound when hit from above
            switch (collider.transform.parent.parent.name)
            {
                case "RightControllerAnchor":
                    if (OVRInput.GetLocalControllerVelocity(rightController).y < 0)
                    {
                        key.Play();
                        //OVRInput.SetControllerVibration(vibrationFrequency, vibrationAmplitude, rightController);
                    }
                    break;
                case "LeftControllerAnchor":
                    if (OVRInput.GetLocalControllerVelocity(leftController).y < 0)
                    {
                        key.Play();
                        //OVRInput.SetControllerVibration(vibrationFrequency, vibrationAmplitude, leftController);
                    }
                    break;
            }
        
    }
}

