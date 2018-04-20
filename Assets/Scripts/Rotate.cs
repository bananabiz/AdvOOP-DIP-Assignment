using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour 
{

    public float rotationSpeed = 0.5f;

	void Start () {
		
	}
	
	void Update () 
    {
        // spin the object
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); 
	}
}
