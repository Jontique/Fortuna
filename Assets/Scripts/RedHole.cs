using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHole : MonoBehaviour {

    public GameObject activeBall;
    public GameManager gm;
    public BallBehaviour ballbehav;

    private Transform hole;

    private Vector3 stopPos;

    public int score;
    private float maxSpeed = 35f;
    private float rand;



	void Start ()
    {
        hole = gameObject.transform.GetChild(0).transform;
        stopPos = hole.position;
        activeBall = gm.GetActiveBall();
        rand = Random.Range(0.0f, 1.0f);

	}
	
	void Update ()
    {
    }



    public bool CheckHoleStay(float speed)
    {
        float chance = 1.0f - (speed / maxSpeed);
         if (chance < rand)
             return true;
        print("chance = " + chance + ", rand = " +rand);
        rand = Random.Range(0.0f, 1.0f);
        return false;
    }




}
