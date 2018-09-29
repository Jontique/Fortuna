using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class LaunchBall : MonoBehaviour {


    Rigidbody rb;

    public float debug_launchSpeed = 20.0f;


	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            //DEBUG
            rb.velocity = new Vector3(0, debug_launchSpeed);
        }

	}


    public void Launch(Vector3 launchVector) //possibility to launc into a direction
    {
        //print(launchVector);
        rb.velocity = new Vector3(0, launchVector.y);
    }




}
