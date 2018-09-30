using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHole : MonoBehaviour {

    public GameObject activeBall;
    public GameManager gm;
    public BallBehaviour ballbehav;

    private Transform hole;

    private Vector3 stopPos;


    private float clampedSpeed;

	void Start ()
    {
        hole = gameObject.transform.GetChild(0).transform;
        stopPos = hole.position;
        activeBall = gm.GetActiveBall();

	}
	
	void Update ()
    {

    }


    public bool CheckHoleStay(float speed)
    {

        return false;
    }

}
