using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class LaunchBall : MonoBehaviour {


    Rigidbody rb;

    public float launchSpeed = 20.0f;


	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, launchSpeed);

        }

	}




}
