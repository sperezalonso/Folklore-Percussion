using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingStructureRotator : MonoBehaviour
{
    [SerializeField]
    private GameObject RoofStructure;
    private Vector3 axis;        // axis by which the ring structure will rotate
    private float speed;        // speed of rotation
    
	void Start ()
	{
	    axis = Vector3.up;
	    speed = Time.deltaTime;
	}
	
	void Update () {
	    // Rotate the ring structure in circles around the center of the scene
	    transform.RotateAround(RoofStructure.transform.position, axis, speed);
	}
}
